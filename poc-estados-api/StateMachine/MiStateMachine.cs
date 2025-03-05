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
        var transiciones = _ctorepo.ObtenerTransiciones().ToList();

       // Verificar si las transiciones están cargando correctamente
        Console.WriteLine($"Total de transiciones cargadas: {transiciones.Count}");

        foreach (var t in transiciones)
        {
            Console.WriteLine($"De {t.IdEstadoDesde} -> {t.IdEstadoHasta} con acción '{t.Acciones}'");
        }

        // Evitar errores si no hay transiciones
        if (!transiciones.Any())
        {
            throw new InvalidOperationException("No hay transiciones configuradas en la base de datos.");
        }

        foreach (var t in transiciones)
        {
            if (string.IsNullOrEmpty(t.Acciones))
            {
                throw new InvalidOperationException($"La acción es nula para la transición de {t.IdEstadoDesde} a {t.IdEstadoHasta}");
            }

            _machine.Configure(t.IdEstadoDesde)
                .Permit(t.Acciones, t.IdEstadoHasta);
        }

    }

    public bool PuedeEjecutar(string accion) => _machine.CanFire(accion);

    public void EjecutarAccion(string accion) => _machine.Fire(accion);

    public List<string> ObtenerAccionesPermitidas() => 
        _machine.PermittedTriggers.ToList();
}