using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventPlus.WebAPI.Repositories
{
    public class ComentarioRepository : IComentarioEventoRepository
    {

        private readonly EventContext _context;
        public ComentarioRepository(EventContext context)
        {
            _context = context;

        }
        public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
        {
            return _context.ComentarioEventos
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
        }

        public void Cadastrar(ComentarioEvento comentarioEvento)
        {
            _context.ComentarioEventos.Add(comentarioEvento);
            _context.SaveChanges();
        }

        public void Deletar(Guid IdComentarioEvento)
        {
            ComentarioEvento comentarioEvento= _context.ComentarioEventos.Find(IdComentarioEvento);

            if (comentarioEvento != null)
            {
                _context.ComentarioEventos.Remove(comentarioEvento);
                _context.SaveChanges();
            }
        }

        public List<ComentarioEvento> List(Guid IdEvento)
        {
            return _context.ComentarioEventos.OrderBy(ComentarioEvento => ComentarioEvento.Descricao).ToList();
        }

        public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
        {
            return _context.ComentarioEventos.Where(c=> c.IdEvento == IdEvento && c.Exibe == true).ToList();
        }
    }
}
