using Moq;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler;
using TesteCarrefour.Repository.Interface;

namespace TestProjectTests.Handler;

public class ConsolidacaoHandlerTests
{
    private readonly Mock<IDatabaseRepository> _mockRepository;
    private readonly ConsolidacaoHandler _handler;

    public ConsolidacaoHandlerTests()
    {
        _mockRepository = new Mock<IDatabaseRepository>();
        _handler = new ConsolidacaoHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task ObterConsolidado_DeveRetornarListaDeConsolidados()
    {
        // Arrange
        var consolidados = new List<Consolidado>();
        _mockRepository.Setup(repo => repo.ObterConsolidado()).ReturnsAsync(consolidados);

        // Act
        var resultado = await _handler.ObterConsolidado();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(consolidados, resultado);
    }

    [Fact]
    public async Task ObterConsolidado_DeveRetornarListaVaziaSeNaoExistirDados()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.ObterConsolidado()).ReturnsAsync(new List<Consolidado>());

        // Act
        var resultado = await _handler.ObterConsolidado();

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    [Fact]
    public async Task ObterConsolidadoPorData_DeveRetornarConsolidadoSeExistir()
    {
        // Arrange
        var date = DateTime.UtcNow;
        var consolidados = new List<Consolidado> ();
        _mockRepository.Setup(repo => repo.ObterConsolidado(date)).ReturnsAsync(consolidados);

        // Act
        var resultado = await _handler.ObterConsolidadoPorData(date);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(consolidados, resultado);
    }

    [Fact]
    public async Task ObterConsolidadoPorData_DeveRetornarListaVaziaSeNaoHouverDados()
    {
        // Arrange
        var date = DateTime.UtcNow;
        _mockRepository.Setup(repo => repo.ObterConsolidado(date)).ReturnsAsync(new List<Consolidado>());

        // Act
        var resultado = await _handler.ObterConsolidadoPorData(date);

        // Assert
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }
}
