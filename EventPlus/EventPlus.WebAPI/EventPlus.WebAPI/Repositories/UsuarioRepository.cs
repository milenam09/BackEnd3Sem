using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Busca o usuario pleo email e valida o hash pela senha 
    /// </summary>
    /// <param name="Email">email do usuario</param>
    /// <param name="Senha">senha do usuario</param>
    /// <returns>usuario buscado e validado</returns>
    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        ///primeiro, buscamos o usuario por email
        var usuarioBuscado = _context.Usuarios
            .Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.Email == Email);

        ///Verifica se o usuario realmente existe
        if(usuarioBuscado != null)
        {
            //comparamos o hash da senha digitada com o q esta no banco

            bool confere = Criptografia.CompararHash
                (Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
            
        }

        return null!;
    }
    /// <summary>
    /// Busca um usuario pelo Id, incluindo os dados do seu tipo de usuario
    /// </summary>
    /// <param name="IdUsuario">Id do usuario a ser buscado</param>
    /// <returns>Usuario buscado</returns>
    public Usuario BuscarPorId(Guid IdUsuario)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation)
            .FirstOrDefault(usuario => usuario.IdUsuario == IdUsuario)!;
    }
    /// <summary>
    /// Cadastra um novo usuario com a senha criptografada
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>
    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
}
