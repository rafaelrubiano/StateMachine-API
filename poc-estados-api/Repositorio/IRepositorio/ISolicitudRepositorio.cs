using poc_estados_api.Models;

namespace poc_estados_api.Repositorio.IRepositorio;

public interface ISolicitudRepositorio
{
    ICollection<Solicitud> GetSolicitudes();
    Solicitud GetSolicitudById(int IdSolicitud);

    Solicitud GetEstadosDeSolicitud(int IdSolicitud);
    bool ExisteSolicitud(int IdSolicitud);
    bool CrearSolicitud(Solicitud solicitud);
    bool GuardarSolicitud();
}