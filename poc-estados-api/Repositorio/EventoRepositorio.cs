using poc_estados_api.Data;
using poc_estados_api.Models;
using poc_estados_api.Repositorio.IRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace poc_estados_api.Repositorio
{
    public class EventoRepositorio : IEventoRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public EventoRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public ICollection<Evento> GetEventos()
        {
            return _bd.Eventos.OrderBy(e => e.FechaHora).ToList();
        }

        public Evento GetEventoById(int id)
        {
            return _bd.Eventos.FirstOrDefault(e => e.IdEvento == id);
        }

        public ICollection<Evento> GetEventosBySolicitud(int idSolicitud)
        {
            return _bd.Eventos.Where(e => e.IdSolicitud == idSolicitud).OrderBy(e => e.FechaHora).ToList();
        }

        public bool CrearEvento(Evento evento)
        {
            evento.Creado = DateTime.Now;
            _bd.Eventos.Add(evento);
            return GuardarEvento();
        }

        public bool GuardarEvento()
        {
            return _bd.SaveChanges() > 0;
        }

        public bool RegistrarEvento(int idSolicitud, int idAccion, int idEstadoDesde, int idEstadoHasta, string creadoPor,
            string observaciones)
        {
            var evento = new Evento
            {
                IdSolicitud = idSolicitud,
                IdAccion = idAccion,
                IdEstadoDesde = idEstadoDesde,
                IdEstadoHasta = idEstadoHasta,
                IdComun = DateTime.Now.Ticks,
                FechaHora = DateTime.Now,
                Observaciones = observaciones,
                CreadoPor = creadoPor,
                Creado = DateTime.Now
            };

            _bd.Eventos.Add(evento);
            return GuardarEvento();
        }
    }
}