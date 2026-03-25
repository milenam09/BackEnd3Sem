using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly IComentarioEventoRepository _comentarioEventoRepository;
    private readonly ContentSafetyClient _contentSafetyClient;
    public ComentarioEventoController(ContentSafetyClient contentSafetyClient,
        IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado nao pode estar vazio");
            }
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(c => c.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                IdEvento = comentarioEvento.IdEvento,
                IdUsuario = comentarioEvento.IdUsuario,
                Descricao = comentarioEvento.Descricao,
                Exibe = !temConteudoImproprio,
                DataComentarioEvento = DateTime.Now
            };

            _comentarioEventoRepository.Cadastrar(novoComentario);
            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public IActionResult List(Guid Id)
    {
        try
        {
            return Ok(_comentarioEventoRepository.List(Id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpGet("Exibe")]
    public IActionResult ListarSomenteExibe(Guid Id)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(Id));
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorIdUsuario(Guid Idusuario, Guid Idevento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.BuscarPorIdUsuario(Idusuario, Idevento));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);

        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _comentarioEventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }

    }
}

