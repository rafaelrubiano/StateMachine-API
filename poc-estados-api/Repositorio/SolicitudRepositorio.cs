using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.Repositorio.IRepositorio;

namespace poc_estados_api.Repositorio;

public class SolicitudRepositorio: ISolicitudRepositorio
{
    
    private readonly ApplicationDbContext _bd;
    
    public SolicitudRepositorio(ApplicationDbContext bd) => _bd = bd;
    public ICollection<Solicitud> GetSolicitudes()
    {
        return _bd.Solicitudes.OrderBy(c => c.IdSolicitud).ToList();
    }

    public Solicitud GetSolicitudById(int IdSolicitud)
    {
        return _bd.Solicitudes.FirstOrDefault(c => c.IdSolicitud == IdSolicitud);
    }

    public Solicitud GetEstadosDeSolicitud(int IdSolicitud)
    {
        throw new NotImplementedException();
    }

    public bool ExisteSolicitud(int IdSolicitud)
    {
        bool valor = _bd.Solicitudes.Any(e => e.IdSolicitud == IdSolicitud);
        return valor;
    }

    public bool CrearSolicitud(Solicitud solicitud)
    {
        solicitud.Creado = DateTime.Now;
        _bd.Solicitudes.Add(solicitud);
        return GuardarSolicitud();
    }

    public bool GuardarSolicitud()
    {
        return _bd.SaveChanges() > 0 ? true : false;
    }
}