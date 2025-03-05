using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace poc_estados_api.Models;

public class Solicitud
{
    [Key]
    public int IdSolicitud { get; set; }
    [ForeignKey("Estado")]
    public int IdEstado { get; set; }
    public string UsuarioRed { get; set; }
    public string UsuarioNombre { get; set; }
    public DateTime? Creado { get; set; }
    // Lista de estados por los que ha pasado la solicitud
    public List<Estado> Estados { get; set; } = new();
    // Nueva propiedad para almacenar los eventos históricos
    public List<AccionEstado> HistorialEventos { get; set; } = new();
}