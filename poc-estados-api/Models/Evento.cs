using System.ComponentModel.DataAnnotations;

namespace poc_estados_api.Models;

public class Evento
{
    [Key]
    public int IdEvento { get; set; }
    public int IdAccion { get; set; }
    public int IdEstadoHasta { get; set; }
    public int IdEstadoDesde { get; set; }
    public long IdComun { get; set; }
    public DateTime? FechaHora { get; set; }
    public string Observaciones { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? Creado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime? Modificado { get; set; }
}