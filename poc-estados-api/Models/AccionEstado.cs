using System.ComponentModel.DataAnnotations;

namespace poc_estados_api.Models;

public class AccionEstado
{
    [Key]
    public int IdAccion { get; set; }
    public int IdEstadoDesde { get; set; }
    public int IdEstadoHasta { get; set; }
    public string GeneraEvento { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? Creado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime? Modificado { get; set; }
}