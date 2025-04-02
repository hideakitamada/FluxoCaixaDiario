using Microsoft.EntityFrameworkCore;
using TesteCarrefour.Entity;
using TesteCarrefour.Repository;
using TesteCarrefour.Services;

namespace TestProjectTests.Repository;

public class DatabaseRepositoryTests
{
    private readonly LancamentoDbContext _context;
    private readonly DatabaseRepository _repository;

    public DatabaseRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LancamentoDbContext>()
            .UseInMemoryDatabase(databaseName: "LancamentoDb")
            .Options;
        _context = new LancamentoDbContext(options);
        _repository = new DatabaseRepository(_context);
    }

    [Fact]
    public async Task CriarLancamentoAsync_DeveAdicionarLancamento()
    {
        // Arrange
        var lancamento = new Lancamento { Id = 0, Valor = 100, DataLancamento = DateTime.UtcNow, Tipo = TesteCarrefour.Entity.Enum.TipoLancamento.Credito };

        // Act
        var resultado = await _repository.CriarLancamentoAsync(lancamento);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(lancamento.Valor, resultado.Valor);
        Assert.Equal(lancamento.Tipo, resultado.Tipo);
    }

    [Fact]
    public async Task ObterTodosLancamentosAsync_DeveRetornarListaLancamentos()
    {
        // Arrange
        _context.Lancamentos.Add(new Lancamento { Id = 1, Valor = 100, Tipo = TesteCarrefour.Entity.Enum.TipoLancamento.Credito, DataLancamento = DateTime.Now });
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.ObterTodosLancamentosAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Single(resultado);
    }

    [Fact]
    public async Task ObterLancamentoPorIdAsync_DeveRetornarLancamentoSeExistir()
    {
        // Arrange
        var lancamento = new Lancamento { Id = 0, Valor = 100 };
        _context.Lancamentos.Add(lancamento);
        await _context.SaveChangesAsync();

        // Act
        var id = _context.Lancamentos.LastOrDefault().Id;
        var resultado = await _repository.ObterLancamentoPorIdAsync(id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(lancamento, resultado);
    }

    [Fact]
    public async Task ObterLancamentoPorIdAsync_DeveRetornarNuloSeNaoExistir()
    {
        // Act
        var resultado = await _repository.ObterLancamentoPorIdAsync(99);

        // Assert
        Assert.Null(resultado);
    }
}
