using poc_estados_api.Models;

namespace poc_estados_api.Repositorio.IRepositorio;

public interface IEstadoRepositorio
{
    ICollection<Estado> GetEstados();
    Estado GetEstadoById(int IdEstado);
    bool ExisteEstado(int IdEstado);
    bool ExisteEstado(string Nombre);
    bool CrearEstado(Estado estado);
    bool ActualizarEstado(Estado estado);
    IEnumerable<(int IdEstadoDesde, string Accion, int IdEstadoHasta)> GetTransiciones();
    bool BorrarEstado(Estado estado);
    bool GuardarEstado();
}