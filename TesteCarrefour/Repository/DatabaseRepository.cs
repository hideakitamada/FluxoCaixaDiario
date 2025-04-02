using Microsoft.EntityFrameworkCore;
using TesteCarrefour.Entity;
using TesteCarrefour.Repository.Interface;
using TesteCarrefour.Services;

namespace TesteCarrefour.Repository;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly LancamentoDbContext _context;

    public DatabaseRepository(LancamentoDbContext context) => _context = context;

    public async Task<Lancamento> CriarLancamentoAsync(Lancamento lancamento)
    {
        await _context.Lancamentos.AddAsync(lancamento);
        await _context.SaveChangesAsync();
        return lancamento;
    }

    /// <summary>
    /// Obter todos lançamentos consolidados por dia
    /// </summary>
    /// <returns></returns>
    public async Task<List<Consolidado>> ObterConsolidado()
    {
        return await _context.Lancamentos.GroupBy(x => x.DataLancamento.Date)
                                         .Select(z => new Consolidado(z.Where(c => c.Tipo == Entity.Enum.TipoLancamento.Credito).Sum(m => m.Valor),
                                                                      z.Where(d => d.Tipo == Entity.Enum.TipoLancamento.Debito).Sum(m => m.Valor),
                                                                      z.Key)).ToListAsync();
    }

    /// <summary>
    /// Obter lançamentos consolidados por data
    /// </summary>
    /// <param name="data">Data utilzizada para o Filtro</param>
    /// <returns></returns>
    public async Task<List<Consolidado>> ObterConsolidado(DateTime data)
    {
        return await _context.Lancamentos.Where(x => x.DataLancamento.Date == data.Date)
                                         .GroupBy(x => x.DataLancamento.Date)
                                         .Select(z => new Consolidado(z.Where(c => c.Tipo == Entity.Enum.TipoLancamento.Credito).Sum(m => m.Valor),
                                                                      z.Where(d => d.Tipo == Entity.Enum.TipoLancamento.Debito).Sum(m => m.Valor),
                                                                      z.Key)).ToListAsync();
    }

    /// <summary>
    /// Obter todos lançamentos
    /// </summary>
    /// <returns></returns>
    public async Task<List<Lancamento>> ObterTodosLancamentosAsync() => await _context.Lancamentos.ToListAsync();

    /// <summary>
    /// Obter lançamento por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Lancamento> ObterLancamentoPorIdAsync(int id) => await _context.Lancamentos.FindAsync(id);
}