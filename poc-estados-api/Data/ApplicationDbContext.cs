using Microsoft.EntityFrameworkCore;
using poc_estados_api.Models;

namespace poc_estados_api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Accion> Acciones { get; set; }
    public DbSet<AccionEstado> AccionesEstado { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Solicitud> Solicitudes { get; set; }
}