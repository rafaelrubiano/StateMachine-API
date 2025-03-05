using Microsoft.EntityFrameworkCore;
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
        return _bd.Solicitudes
            .Include(s => s.Estados) // Cargar la lista de estados
            .Include(s => s.HistorialEventos) // Cargar eventos históricos (AccionEstado)
            .FirstOrDefault(c => c.IdSolicitud == IdSolicitud);
    }

    public Solicitud GetEstadosDeSolicitud(int IdSolicitud)
    {
        throw new NotImplementedException();
    }

    public bool ActualizarSolicitud(Solicitud solicitud)
    {
        var solicitudExistente = _bd.Solicitudes.Find(solicitud.IdSolicitud);
        if (solicitudExistente != null)
        {
            _bd.Entry(solicitudExistente).CurrentValues.SetValues(solicitud);
        }
        else
        {
            _bd.Solicitudes.Update(solicitud);
        }

        return GuardarSolicitud();
    }

    public bool ExisteSolicitud(int IdSolicitud)
    {
        bool valor = _bd.Solicitudes.Any(e => e.IdSolicitud == IdSolicitud);
        return valor;
    }

    public bool CrearSolicitud(Solicitud solicitud)
    {
        try
        {
            solicitud.Creado = DateTime.Now;
            _bd.Solicitudes.Add(solicitud);
            bool guardado = GuardarSolicitud();
        
            // Verifica que se haya generado el ID después de guardar
            if (guardado && solicitud.IdSolicitud > 0)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear solicitud: {ex.Message}");
            return false;
        }
    }

    public bool GuardarSolicitud()
    {
        return _bd.SaveChanges() > 0 ? true : false;
    }
}