using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.Repositorio.IRepositorio;

namespace poc_estados_api.Repositorio;

public class EstadoRepositorio: IEstadoRepositorio
{
    private readonly ApplicationDbContext _bd;
    
    public EstadoRepositorio(ApplicationDbContext bd) => _bd = bd;
    
    public ICollection<Estado> GetEstados()
    {
        return _bd.Estados.OrderBy(c => c.Nombre).ToList();
    }

    public Estado GetEstadoById(int IdEstado)
    {
        return _bd.Estados.FirstOrDefault(c => c.IdEstado == IdEstado);
    }

    public bool ExisteEstado(int IdEstado)
    {
        return _bd.Estados.Any(e => e.IdEstado == IdEstado);
    }

    public bool ExisteEstado(string Nombre)
    {
        bool valor = _bd.Estados.Any(e => e.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());
        return valor;
    }

    public bool CrearEstado(Estado estado)
    {
        estado.Creado = DateTime.Now;
        _bd.Estados.Add(estado);
        return GuardarEstado();
    }

    public bool ActualizarEstado(Estado estado)
    {
        estado.Creado = DateTime.Now;
        
        var estadoExiste = _bd.Estados.Find(estado.IdEstado);
        if (estadoExiste != null)
        {
            _bd.Entry(estadoExiste).CurrentValues.SetValues(estado);
        }
        else
        {
            _bd.Estados.Update(estado);
        }

        return GuardarEstado();
    }

    public IEnumerable<(int IdEstadoDesde, string Accion, int IdEstadoHasta)> GetTransiciones()
    {
        return _bd.AccionesEstado
            .Join(_bd.Acciones, 
                ae => ae.IdAccion, 
                a => a.IdAccion, 
                (ae, a) => new 
                {
                    ae.IdEstadoDesde,
                    Accion = a.Descripcion ?? "", // Evitar valores nulos
                    ae.IdEstadoHasta
                })
            .ToList()
            .Select(t => (t.IdEstadoDesde, t.Accion, t.IdEstadoHasta));
    }

    public bool BorrarEstado(Estado estado)
    {
        _bd.Estados.Remove(estado);
        return GuardarEstado();
    }

    public bool GuardarEstado()
    {
        return _bd.SaveChanges() > 0 ? true : false;
    }
}