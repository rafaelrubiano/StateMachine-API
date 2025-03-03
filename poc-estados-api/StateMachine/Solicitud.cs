/*using System;
using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.StateMachine.IStateMachine;

namespace poc_estados_api.StateMachine;

public class Solicitud
{
    private readonly ApplicationDbContext _context;
    private IEstado _estado;

    public Solicitud(ApplicationDbContext context)
    {
        _context = context;
        _estado = new EstadoCreado();
    }

    public Estado.EstadoSolicitud EstadoActual => _estado.Estado;

    /*public async Task CambiarEstado(Accion.AccionSolicitud accion)
    {
        var nuevoEstado = await _estado.EjecutarAccion(accion);
        _estado = nuevoEstado switch
        {
            Estado.EstadoSolicitud.EnRevision => new EstadoEnRevision(),
            Estado.EstadoSolicitud.Aprobado => new EstadoAprobado(),
            Estado.EstadoSolicitud.Liberado => new EstadoLiberado(),
            Estado.EstadoSolicitud.Cerrado => throw new InvalidOperationException("No se pueden hacer más cambios."),
            _ => throw new InvalidOperationException("Estado desconocido")
        };
            
        await GuardarEstadoEnBD();
    }#1#

    /*private async Task GuardarEstadoEnBD()
    {
        var registro = new EstadoRegistro { Estado = _estado.Estado.ToString(), FechaCambio = DateTime.UtcNow };
        _context.Estados.Add(registro);
        await _context.SaveChangesAsync();
    }#1#
}*/