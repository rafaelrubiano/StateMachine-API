using poc_estados_api.Models;

namespace poc_estados_api.Repositorio.IRepositorio;

public interface IEventoRepositorio
{
    ICollection<Evento> GetEventos();
    Evento GetEventoById(int id);
    ICollection<Evento> GetEventosBySolicitud(int idSolicitud);
    bool CrearEvento(Evento evento);
    bool GuardarEvento();
    bool RegistrarEvento(int idSolicitud, int idAccion, int idEstadoDesde, int idEstadoHasta, string creadoPor, string observaciones);
}