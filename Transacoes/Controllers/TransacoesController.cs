using Aplicacao.Interfaces;
using Dominio.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Transacoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {

        private readonly ITransacaoService _service;

        public TransacoesController(ITransacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? inicio,
            [FromQuery] DateTime? fim,
            [FromQuery] long? categoriaId,
            [FromQuery] int? tipo)
        {
            var result = await _service.ListarAsync(inicio, fim, categoriaId, tipo);
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.ObterPorIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarTransacaoRequest request)
        {
            var id = await _service.CriarAsync(request);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] AtualizarTransacaoRequest request)
        {
            await _service.AtualizarAsync(id, request);
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeletarAsync(id);
            return Ok();
        }
    }
}
