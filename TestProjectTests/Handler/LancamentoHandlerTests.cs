using Moq;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler;
using TesteCarrefour.Repository.Interface;

namespace TestProjectTests.Handler;

public class LancamentoHandlerTests
{
    private readonly Mock<IDatabaseRepository> _mockRepository;
    private readonly LancamentoHandler _handler;

    public LancamentoHandlerTests()
    {
        _mockRepository = new Mock<IDatabaseRepository>();
        _handler = new LancamentoHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task CriarLancamentoAsync_DeveRetornarLancamentoCriado()
    {
        // Arrange
        var lancamentoDados = new LancamentoDados { Valor = 100, DataLancamento = DateTime.UtcNow, Tipo = TesteCarrefour.Entity.Enum.TipoLancamento.Credito };
        var lancamento = new Lancamento { Id = 1, Valor = 100, DataLancamento = lancamentoDados.DataLancamento, Tipo = lancamentoDados.Tipo };
        _mockRepository.Setup(repo => repo.CriarLancamentoAsync(It.IsAny<Lancamento>())).ReturnsAsync(lancamento);

        // Act
        var resultado = await _handler.CriarLancamentoAsync(lancamentoDados);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(lancamento.Valor, resultado.Valor);
        Assert.Equal(lancamento.Tipo, resultado.Tipo);
    }

    [Fact]
    public async Task ObterTodosLancamentosAsync_DeveRetornarListaLancamentos()
    {
        // Arrange
        var lancamentos = new List<Lancamento> { new Lancamento { Id = 1, Valor = 100 } };
        _mockRepository.Setup(repo => repo.ObterTodosLancamentosAsync()).ReturnsAsync(lancamentos);

        // Act
        var resultado = await _handler.ObterTodosLancamentosAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(lancamentos, resultado);
    }

    [Fact]
    public async Task ObterLancamentoPorIdAsync_DeveRetornarLancamentoSeExistir()
    {
        // Arrange
        var lancamento = new Lancamento { Id = 1, Valor = 100 };
        _mockRepository.Setup(repo => repo.ObterLancamentoPorIdAsync(1)).ReturnsAsync(lancamento);

        // Act
        var resultado = await _handler.ObterLancamentoPorIdAsync(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(lancamento, resultado);
    }

    [Fact]
    public async Task ObterLancamentoPorIdAsync_DeveLancarExcecaoSeNaoExistir()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.ObterLancamentoPorIdAsync(1)).ReturnsAsync((Lancamento)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.ObterLancamentoPorIdAsync(1));
    }
}