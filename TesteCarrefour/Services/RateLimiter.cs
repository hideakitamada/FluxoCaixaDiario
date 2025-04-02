using System.Collections.Concurrent;

namespace TesteCarrefour.Services;

public class RateLimiter(int maxRequestsPerSecond)
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(maxRequestsPerSecond, maxRequestsPerSecond);
    private readonly TimeSpan _timeFrame = TimeSpan.FromSeconds(1);
    private readonly ConcurrentQueue<DateTime> _requestTimestamps = new();

    public async Task<bool> RequestAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            // Remover requisições antigas
            while (_requestTimestamps.TryPeek(out var timestamp) && DateTime.UtcNow - timestamp > _timeFrame)
            {
                _requestTimestamps.TryDequeue(out _);
            }

            // Verifica se atingiu o limite
            if (_requestTimestamps.Count >= _semaphore.CurrentCount)
            {
                return false; // Bloqueia requisições acima do limite
            }

            _requestTimestamps.Enqueue(DateTime.UtcNow);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}