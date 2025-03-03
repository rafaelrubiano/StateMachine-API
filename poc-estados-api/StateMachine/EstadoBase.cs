/*using AutoMapper;
using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;
using poc_estados_api.StateMachine.IStateMachine;

namespace poc_estados_api.StateMachine;

public abstract class EstadoBase : IEstado
{
    /*private readonly IMapper _mapper;

    public EstadoBase(IMapper mapper)
    {
        _mapper = mapper;
    }#1#
    public abstract EstadoDto Estado { get; }
    protected Dictionary<Accion.AccionSolicitud, Estado> Transiciones { get; } = new();

    public EstadoDto EstadoDto { get; }

    public virtual Task<EstadoDto> EjecutarAccion(Accion.AccionSolicitud accion)
    {
        if (Transiciones.TryGetValue(accion, out EstadoDto nuevoEstado))
        {
            return Task.FromResult<EstadoDto>(nuevoEstado);
        }
        throw new InvalidOperationException($"La acción {accion} no es válida desde el estado {Estado}.");
    }
}*/