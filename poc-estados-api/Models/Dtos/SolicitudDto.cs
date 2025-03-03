namespace poc_estados_api.Models.Dtos;

public class SolicitudDto
{
    public int IdSolicitud { get; set; }
    public int IdEstado { get; set; }
    public string UsuarioRed { get; set; }
    public string UsuarioNombre { get; set; }
    public DateTime? Creado { get; set; }
    public List<Estado> Estados { get; set; } = new();
}