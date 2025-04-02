using Microsoft.AspNetCore.Mvc;
using Moq;
using TesteCarrefour.Controllers;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;

namespace TestProjectTests.Controllers;

public class LancamentosControllerTests
{
    private readonly Mock<ILancamentoHandler> _mockHandler;
    private readonly LancamentosController _controller;

    public LancamentosControllerTests()
    {
        _mockHandler = new Mock<ILancamentoHandler>();
        _controller = new LancamentosController(_mockHandler.Object);
    }

    [Fact]
    public async Task Criar_DeveRetornarCreatedComLancamento()
    {
        // Arrange
        var lancamentoDados = new LancamentoDados();
        var lancamentoCriado = new Lancamento { Id = 1 };
        _mockHandler.Setup(h => h.CriarLancamentoAsync(lancamentoDados)).ReturnsAsync(lancamentoCriado);

        // Act
        var resultado = await _controller.Criar(lancamentoDados) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(201, resultado.StatusCode);
        Assert.Equal("ObterPorId", resultado.ActionName);
        Assert.Equal(1, ((Lancamento)resultado.Value).Id);
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarListaDeLancamentos()
    {
        // Arrange
        var lancamentos = new List<Lancamento> { new Lancamento() };
        _mockHandler.Setup(h => h.ObterTodosLancamentosAsync()).ReturnsAsync(lancamentos);

        // Act
        var resultado = await _controller.ObterTodos() as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);
        Assert.Equal(lancamentos, resultado.Value);
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarListaVaziaSeNaoHouverLancamentos()
    {
        // Arrange
        _mockHandler.Setup(h => h.ObterTodosLancamentosAsync()).ReturnsAsync(new List<Lancamento>());

        // Act
        var resultado = await _controller.ObterTodos() as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);
        Assert.Empty((List<Lancamento>)resultado.Value);
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarLancamentoSeExistir()
    {
        // Arrange
        var lancamento = new Lancamento { Id = 1 };
        _mockHandler.Setup(h => h.ObterLancamentoPorIdAsync(1)).ReturnsAsync(lancamento);

        // Act
        var resultado = await _controller.ObterPorId(1) as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);
        Assert.Equal(lancamento, resultado.Value);
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarNotFoundSeNaoExistir()
    {
        // Arrange
        _mockHandler.Setup(h => h.ObterLancamentoPorIdAsync(1)).ReturnsAsync((Lancamento)null);

        // Act
        var resultado = await _controller.ObterPorId(1);

        // Assert
        Assert.IsType<NotFoundResult>(resultado);
    }
}
