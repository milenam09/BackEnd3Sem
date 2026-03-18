using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoEventoRepository : ITipoEventoRepository
{
    private readonly EventContext _context;

    public TipoEventoRepository(EventContext context)
    {
        _context = context; 
    }

    /// <summary>
    /// Atualiza um tipo de evento usado o rastreamento automatico
    /// </summary>
    /// <param name="id"> id do tipo evento a ser ataualizado</param>
    /// <param name="tipoEvento">Novos dados do tipo evento</param>
    public void Atualizar(Guid id, TipoEvento tipoEvento)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);
        if (tipoEventoBuscado != null)
        {
            tipoEventoBuscado.Titulo = tipoEvento.Titulo;

            _context.SaveChanges();
        }
    }
    /// <summary>
    /// busca por tipo de evento por Id
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>
    /// <returns>Objeto do tipoEvento com as informacoes do tipo evento buscado</returns>
    public TipoEvento BuscarPorId(Guid id)
    {
        return _context.TipoEventos.Find(id);
    }
    /// <summary>
    /// Cadastrar um novo tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    public void Cadastrar(TipoEvento tipoEvento)
    {
        _context.TipoEventos.Add(tipoEvento);
        _context.SaveChanges();
    }
    /// <summary>
    /// Deletar um tipo de evento
    /// </summary>
    /// <param name="id">id do tipo evento a ser deletado</param>

    public void Deletar(Guid id)
    {
        var tipoEventoBuscado = _context.TipoEventos.Find(id);
        if (tipoEventoBuscado != null)
        {
            _context.TipoEventos.Remove(tipoEventoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public List<TipoEvento> Listar()
    {
        return _context.TipoEventos
            .OrderBy(tipoEvento => tipoEvento.Titulo)
            .ToList();
    }
}
