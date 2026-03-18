using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class PresencaRepository : IPresencaRepository
    {
        private readonly EventContext _context;

        public PresencaRepository(EventContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo que alterna a situacao da presenca
        /// </summary>
        /// <param name="id">Id da presenca a ser alterada</param>
        public void Atualizar(Guid id)
        {
            var presencaBuscada = _context.Presencas.Find(id);

            if (presencaBuscada != null)
            {
                presencaBuscada.Situacao = !presencaBuscada.Situacao;

                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo que busca uma presenca por id
        /// </summary>
        /// <param name="id">Id da presenca a ser buscada</param>
        /// <returns>Presenca buscada</returns>
        public Presenca BuscarPorId(Guid id)
        {
            return _context.Presencas
                  .Include(p => p.IdEventoNavigation)
                   .ThenInclude(e => e!.IdInstuicaoNavigation)
                    .FirstOrDefault(p => p.IdPresenca == id)!;
        }

        public void Deletar(Guid id)
        {
            var presencaBuscada = _context.Presencas.Find(id);

            if (presencaBuscada != null)
            {
                _context.Presencas.Remove(presencaBuscada);
                _context.SaveChanges();
            }
        }

        public void Inscrever(Presenca presenca)
        {
            _context.Presencas.Add(presenca);
            _context.SaveChanges();
        }

        public List<Presenca> Listar()
        {
            return _context.Presencas.OrderBy(Presenca => Presenca).ToList();
        }

        /// <summary>
        /// Metodo que a lista as presecas de um usuario especifico
        /// </summary>
        /// <param name="IdUsuario">id do usuario para filtragem</param>
        /// <returns>Lista de presencas de um usuario</returns>
        public List<Presenca> ListarMinhas(Guid IdUsuario)
        {
            return _context.Presencas
                .Include(p => p.IdEventoNavigation)
                .ThenInclude(e => e!.IdInstuicaoNavigation)
                .Where(p => p.IdUsuario == IdUsuario)
                .ToList();       
        }
    }
}
