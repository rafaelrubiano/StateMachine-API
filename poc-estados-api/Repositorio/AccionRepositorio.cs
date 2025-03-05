using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;
using poc_estados_api.Repositorio.IRepositorio;

namespace poc_estados_api.Repositorio;

public class AccionRepositorio : IAccionRepositorio
{
    private readonly ApplicationDbContext _context;

    public AccionRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Accion> GetAcciones()
    {
        throw new NotImplementedException();
    }

    public Accion GetAccionById(int AccionId)
    {
        throw new NotImplementedException();
    }

    public bool ExisteAcciones(int IdAccion)
    {
        throw new NotImplementedException();
    }

    public bool AccionCreadoPor(Accion accion)
    {
        throw new NotImplementedException();
    }

    public bool AccionActualizadoPor(Accion accion)
    {
        throw new NotImplementedException();
    }

    public AccionEstadoDto GetAccionEstado(int idSolicitud, string accion)
    {
        var resultadoDto = _context.AccionesEstado
            .Where(a => (idSolicitud == 0 || a.IdSolicitud == idSolicitud) && 
                        (string.IsNullOrEmpty(accion) || a.Acciones == accion))
            .Select(a => new AccionEstadoDto
            {
                IdAccionEstado = a.IdAccionEstado,
                IdSolicitud = a.IdSolicitud,
                IdEstadoDesde = a.IdEstadoDesde,
                IdEstadoHasta = a.IdEstadoHasta,
                GeneraEvento = a.GeneraEvento,
                CreadoPor = a.CreadoPor,
                Creado = a.Creado,
                ModificadoPor = a.ModificadoPor,
                Modificado = a.Modificado,
                Acciones = a.Acciones
            })
            .FirstOrDefault();

        return resultadoDto;
    }

    public void RegistrarAccionEstado(int idSolicitud, int idEstadoDesde, int idEstadoHasta, string acciones, string usuario)
    {
        var accionEstado = new AccionEstado
        {
            IdSolicitud = idSolicitud,
            IdEstadoDesde = idEstadoDesde,
            IdEstadoHasta = idEstadoHasta,
            Acciones = acciones,
            GeneraEvento = "Sí",
            CreadoPor = usuario,
            Creado = DateTime.Now
        };

        _context.AccionesEstado.Add(accionEstado);
        _context.SaveChanges();
    }

    public List<AccionEstadoDto> ObtenerTransiciones()
    {
        return (from ae in _context.AccionesEstado
            join a in _context.Acciones on ae.IdAccionEstado equals a.IdAccion
            select new AccionEstadoDto
            {
                IdEstadoDesde = ae.IdEstadoDesde,
                IdEstadoHasta = ae.IdEstadoHasta,
                Acciones = a.Descripcion 
            }).ToList();
    }
}