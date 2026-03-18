using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }


    /// <summary>
    ///  Atualiza o tipo de Usuario usando o rastreamento automático
    /// </summary>
    /// <param name="Id">Id do tipo Usuario a ser atualizado</param>
    /// <param name="tipoUsuario">Novos dados do tipo Usuario</param>
    public void Atualizar(Guid Id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(Id);

        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca o tipo Usuario pelo ID
    /// </summary>
    /// <param name="id">id do tipo Usuario a ser buscado</param>
    /// <returns>Objeto do tipoUsuario com as informações do tipo de Usuario buscado </returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id);
    }

    /// <summary>
    /// Cadastra um novo tipo de usuario
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuario a ser cadastrado</param>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta tipo de usuario
    /// </summary>
    /// <param name="id">id de usuario a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    public void Listar(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario).ToList();
    }
}




