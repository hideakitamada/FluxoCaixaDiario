using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;

namespace TesteCarrefour.Controllers;

[ApiController]
[Route("api/consolidado")]
[Authorize]

public class ConsolidadoController(IConsolidacaoHandler handler) : ControllerBase
{
    private readonly IConsolidacaoHandler _handler = handler;

    [HttpGet]
    [ProducesResponseType(typeof(List<Consolidado>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterConsolidado()
    {
        return Ok(await _handler.ObterConsolidado());
    }

    [HttpGet("{date}")]
    [ProducesResponseType(typeof(List<Consolidado>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterConsolidado(DateTime date)
    {
        return Ok(await _handler.ObterConsolidadoPorData(date));
    }
}
