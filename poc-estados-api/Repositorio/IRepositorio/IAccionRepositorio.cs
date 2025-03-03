using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;

namespace poc_estados_api.Repositorio.IRepositorio;

public interface IAccionRepositorio
{
    ICollection<Accion> GetAcciones();
    Accion GetAccionById(int AccionId);
    bool ExisteAcciones(int IdAccion);
    bool AccionCreadoPor(Accion accion);
    bool AccionActualizadoPor(Accion accion);
    List<AccionEstadoDto> ObtenerTransiciones();
    
}