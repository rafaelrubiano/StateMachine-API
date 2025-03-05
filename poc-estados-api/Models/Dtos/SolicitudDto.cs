using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poc_estados_api.Models.Dtos;

public class SolicitudDto
{
    [Key]
    public int IdSolicitud { get; set; }
    [ForeignKey("Estado")]
    public int IdEstado { get; set; }
    public string UsuarioRed { get; set; }
    public string UsuarioNombre { get; set; }
    public DateTime? Creado { get; set; }
    // Lista de estados por los que ha pasado la solicitud
    public List<EstadoDto> Estados { get; set; } = new();
    // Nueva propiedad para almacenar los eventos históricos
    public List<AccionEstadoDto> HistorialEstados { get; set; } = new();
}