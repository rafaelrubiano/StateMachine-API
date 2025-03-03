namespace poc_estados_api.Models.Dtos;

public class CrearEstadoDto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Color { get; set; }
    public short? Orden { get; set; }
    public string EsFinal { get; set; }
    public string CreadoPor { get; set; }
    public string DescripcionDiagrama { get; set; }
}