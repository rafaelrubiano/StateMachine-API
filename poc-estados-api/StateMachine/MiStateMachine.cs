using poc_estados_api.Repositorio.IRepositorio;
using Stateless;

namespace poc_estados_api.StateMachine;

public class MiStateMachine
{
    private readonly StateMachine<int, string> _machine;
    private readonly IAccionRepositorio _ctorepo;

    public int EstadoActual => _machine.State;

    public MiStateMachine(int estadoInicial, IAccionRepositorio repo)
    {
        _ctorepo = repo;
        _machine = new StateMachine<int, string>(estadoInicial);

        // 🔹 Cargar transiciones desde la BD
        var transiciones = _ctorepo.ObtenerTransiciones();

        foreach (var t in transiciones)
        {
            _machine.Configure(t.IdEstadoDesde)
                .Permit(t.Acciones, t.IdEstadoHasta);
        }
    }

    public bool PuedeEjecutar(string accion) => _machine.CanFire(accion);

    public void EjecutarAccion(string accion) => _machine.Fire(accion);

    public List<string> ObtenerAccionesPermitidas() => 
        _machine.PermittedTriggers.ToList();
}