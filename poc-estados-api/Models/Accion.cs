using System.ComponentModel.DataAnnotations;

namespace poc_estados_api.Models;

public class Accion
{
    [Key]
    public int IdAccion { get; set; }
    public string? Descripcion { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? Creado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime? Modificado { get; set; }
}