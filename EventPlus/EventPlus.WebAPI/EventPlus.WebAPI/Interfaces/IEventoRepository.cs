using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    void Deletar(Guid id);
    List<Evento> Listar();
    void Atualizar(Guid id, Evento evento);
    Evento BuscarPoId(Guid id);
    List<Evento> ListarPorId(Guid IdUsuario);
    List<Evento> ListarProximos();

}
