using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;

namespace TesteCarrefour.Controllers;

[ApiController]
[Route("api/lancamentos")]
[Authorize]

public class LancamentosController(ILancamentoHandler handler) : ControllerBase
{
    private readonly ILancamentoHandler _handler = handler;
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Criar([FromBody] LancamentoDados lancamento)
    {
        var resultado = await _handler.CriarLancamentoAsync(lancamento);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Lancamento>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        return Ok(await _handler.ObterTodosLancamentosAsync());
    }

    [HttpGet("{id}")]

    [ProducesResponseType(typeof(Lancamento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var lancamento = await _handler.ObterLancamentoPorIdAsync(id);
        if (lancamento == null) return NotFound();
        return Ok(lancamento);
    }
}