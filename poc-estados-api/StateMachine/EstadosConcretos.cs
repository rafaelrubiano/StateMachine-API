/*using poc_estados_api.Models;

namespace poc_estados_api.StateMachine;

public class EstadoCreado : EstadoBase
{
    public override Estado.EstadoSolicitud Estado => Models.Estado.EstadoSolicitud.Creado;
    public EstadoCreado() => Transiciones[Models.Accion.AccionSolicitud.EnviarARevision] = Models.Estado.EstadoSolicitud.EnRevision;
}

public class EstadoEnRevision : EstadoBase
{
    public override Estado.EstadoSolicitud Estado => Models.Estado.EstadoSolicitud.EnRevision;
    public EstadoEnRevision()
    {
        Transiciones[Models.Accion.AccionSolicitud.Aprobar] = Models.Estado.EstadoSolicitud.Aprobado;
        Transiciones[Models.Accion.AccionSolicitud.Rechazar] = Models.Estado.EstadoSolicitud.Rechazado;
    }
}

public class EstadoAprobado : EstadoBase
{
    public override Estado.EstadoSolicitud Estado => Models.Estado.EstadoSolicitud.Aprobado;
    public EstadoAprobado() => Transiciones[Models.Accion.AccionSolicitud.Liberar] = Models.Estado.EstadoSolicitud.Liberado;
}

public class EstadoLiberado : EstadoBase
{
    public override Estado.EstadoSolicitud Estado => Models.Estado.EstadoSolicitud.Liberado;
    public EstadoLiberado() => Transiciones[Models.Accion.AccionSolicitud.Cerrar] = Models.Estado.EstadoSolicitud.Cerrado;
}*/