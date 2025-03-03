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

    public List<AccionEstadoDto> ObtenerTransiciones()
    {
        return (from ae in _context.AccionesEstado
            join a in _context.Acciones on ae.IdAccion equals a.IdAccion
            select new AccionEstadoDto
            {
                IdEstadoDesde = ae.IdEstadoDesde,
                IdEstadoHasta = ae.IdEstadoHasta,
                Acciones = a.Descripcion  // 🔹 Ahora `Accion` es un string
            }).ToList();
    }
}