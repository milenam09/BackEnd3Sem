using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;

    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna uma lista de presensas de um usuario especifico
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>Status code 200 e uma lista de presencas</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarMinhas(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdEvento = presenca.IdEvento,
                IdUsuario = presenca.IdUsuario
            };

            _presencaRepository.Inscrever(novaPresenca);
            return StatusCode(201, novaPresenca);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);    
        }
    }
}
