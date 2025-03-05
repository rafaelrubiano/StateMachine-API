using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poc_estados_api.Models.Dtos;

public class AccionEstadoDto
{
    [Key]
    public int IdAccionEstado { get; set; }
    [ForeignKey("Solicitud")]
    public int IdSolicitud { get; set; }
    public int IdEstadoDesde { get; set; }
    public int IdEstadoHasta { get; set; }
    public string GeneraEvento { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? Creado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime? Modificado { get; set; }
    public string Acciones { get; set; }
}