using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de bsucar em usuario por id 
    /// </summary>
    /// <param name="id">Id do usuario a ser buscado</param>
    /// <returns>Status code 200 e o usuario buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch(Exception error)
        {
            return BadRequest(error.Message);
        }
    }
    /// <summary>
    /// EndPoint da API que faz a chamada para o metodo cadastrar um usuario
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    /// <returns>Status Code 201 e o usuario cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Email = usuario.Email!,
                Senha = usuario.Senha!,
            };

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
