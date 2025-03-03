using System.ComponentModel.DataAnnotations;

namespace poc_estados_api.Models.Dtos;

public class EstadoDto
{
    public int IdEstado { get; set; }
    [MaxLength(100, ErrorMessage = "El número máximo de caracteres es de 100!")]
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Color { get; set; }
    public short? Orden { get; set; }
    public string EsFinal { get; set; }
    public string CreadoPor { get; set; }
    public DateTime? Creado { get; set; }
    public string ModificadoPor { get; set; }
    public DateTime? Modificado { get; set; }
    public string DescripcionDiagrama { get; set; }
}