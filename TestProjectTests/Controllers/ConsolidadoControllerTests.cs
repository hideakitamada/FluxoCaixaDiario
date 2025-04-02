using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteCarrefour.Controllers;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;

namespace TestProjectTests.Controllers;

public class ConsolidadoControllerTests
{
    private readonly Mock<IConsolidacaoHandler> _mockHandler;
    private readonly ConsolidadoController _controller;

    public ConsolidadoControllerTests()
    {
        _mockHandler = new Mock<IConsolidacaoHandler>();
        _controller = new ConsolidadoController(_mockHandler.Object);
    }

    [Fact]
    public async Task ObterConsolidado_DeveRetornarListaDeConsolidados()
    {
        // Arrange
        var consolidadoList = new List<Consolidado>();
        _mockHandler.Setup(h => h.ObterConsolidado()).ReturnsAsync(consolidadoList);

        // Act
        var resultado = await _controller.ObterConsolidado() as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);
        Assert.Equal(consolidadoList, resultado.Value);
    }

    [Fact]
    public async Task ObterConsolidadoPorData_DeveRetornarListaDeConsolidados()
    {
        // Arrange
        var data = DateTime.UtcNow;
        var consolidadoList = new List<Consolidado>();
        _mockHandler.Setup(h => h.ObterConsolidadoPorData(data)).ReturnsAsync(consolidadoList);

        // Act
        var resultado = await _controller.ObterConsolidado(data) as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);
        Assert.Equal(consolidadoList, resultado.Value);
    }
}
