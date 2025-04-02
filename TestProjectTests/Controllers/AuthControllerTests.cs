using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using TesteCarrefour.Controllers;

namespace TestProjectTests.Controllers;

public class AuthControllerTests
{
    [Fact]
    public void GerarToken_DeveRetornarTokenValido()
    {
        // Arrange
        var mockConfig = new Mock<IConfiguration>();

        mockConfig.Setup(x => x["JwtSettings:Secret"]).Returns("SuaChaveSecretaMuitoSeguraCom32CaracteresOuMais!");
        mockConfig.Setup(x => x["JwtSettings:Issuer"]).Returns("test_issuer");
        mockConfig.Setup(x => x["JJwtSettingswt:Audience"]).Returns("test_audience");

        var controller = new AuthController(mockConfig.Object);

        // Act
        var resultado = controller.GerarToken() as OkObjectResult;

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(200, resultado.StatusCode);

        var response = resultado.Value as dynamic;
        Assert.NotNull(response);
    }
}
