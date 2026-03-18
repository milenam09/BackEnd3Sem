using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// Lista todos os eventos cadastrados
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid idUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Cadastra um novo evento
    /// </summary>
    /// <param name="novoEvento">Objeto com as informações do evento</param>
    [HttpPost]
    public IActionResult Cadastrar(EventoDTO evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome!,
                Descricao = evento.Descricao!,
                DataEvento = evento.Data!
            };

            _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201, novoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Deleta um evento existente
    /// </summary>
    /// <param name="id">Id do evento a ser deletado</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Atualiza um evento existente
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="evento">Objeto com as novas informações</param>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, Evento evento)
    {
        try
        {
            _eventoRepository.Atualizar(id, evento);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


}