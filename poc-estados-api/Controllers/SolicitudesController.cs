using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;
using poc_estados_api.Repositorio.IRepositorio;
using poc_estados_api.StateMachine;

namespace poc_estados_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController : ControllerBase
    {
        private readonly IAccionRepositorio _ctAccionRepo;
        private readonly IEstadoRepositorio _ctEstadoRepo;
        private readonly ISolicitudRepositorio _ctSolicitudRepo;
        private readonly IEventoRepositorio _ctEventoRepo;
        private readonly IMapper _mapper;

        public SolicitudesController(IAccionRepositorio ctAccionRepo, IEstadoRepositorio ctEstadoRepo, ISolicitudRepositorio ctSolicitudRepo, IEventoRepositorio ctEventoRepo, IMapper mapper)
        {
            _ctAccionRepo = ctAccionRepo;
            _ctEstadoRepo = ctEstadoRepo;
            _ctSolicitudRepo = ctSolicitudRepo;
            _ctEventoRepo = ctEventoRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult GetSolicitudes()
        {
            var ListaSolicitudes = _ctSolicitudRepo.GetSolicitudes();
            var listaSolicitudDto = new List<SolicitudDto>();
            foreach (var lista in ListaSolicitudes)
            {
                listaSolicitudDto.Add(_mapper.Map<SolicitudDto>(lista));
            }
            return Ok(listaSolicitudDto);
        }
        
        [HttpGet("IdSolicitud", Name = "GetSolicitud")]
        public ActionResult GetSolicitudes([FromQuery] int IdSolicitud)
        {
            var itemSolicitud = _ctSolicitudRepo.GetSolicitudById(IdSolicitud);

            if (itemSolicitud == null)
                return NotFound();

            var itemSolicitudDto = new SolicitudDto
            {
                IdSolicitud = itemSolicitud.IdSolicitud,
                IdEstado = itemSolicitud.IdEstado,
                UsuarioRed = itemSolicitud.UsuarioRed,
                UsuarioNombre = itemSolicitud.UsuarioNombre,
                Creado = itemSolicitud.Creado,
                Estados = itemSolicitud.Estados.Select(e => new EstadoDto
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre,
                    Color = e.Color,
                    Creado = e.Creado
                }).ToList(),
                HistorialEstados = itemSolicitud.HistorialEventos.Select(ae => new AccionEstadoDto
                {
                    IdAccionEstado = ae.IdAccionEstado,
                    IdSolicitud = ae.IdSolicitud,
                    IdEstadoDesde = ae.IdEstadoDesde,
                    IdEstadoHasta = ae.IdEstadoHasta,
                    Acciones = ae.Acciones,
                    Creado = ae.Creado
                }).ToList()
            };

            return Ok(itemSolicitudDto);
        }
        
        [HttpPost("cambiarEstadoSolicitud/{id}")]
        public IActionResult CambiarEstado(int id, [FromBody] CambiarEstadoDto request)
        {
            var solicitud = _ctSolicitudRepo.GetSolicitudById(id);
            if (solicitud == null)
                return NotFound("Solicitud no encontrada.");

            var stateMachine = new MiStateMachine(solicitud.IdEstado, _ctAccionRepo);

            if (string.IsNullOrWhiteSpace(request.Acciones) || string.IsNullOrWhiteSpace(request.Usuario))
                return BadRequest("Acción o usuario inválido.");

            if (!stateMachine.PuedeEjecutar(request.Acciones))
                return BadRequest("Transición no permitida.");

            int estadoAnterior = solicitud.IdEstado;
            stateMachine.EjecutarAccion(request.Acciones);
            solicitud.IdEstado = stateMachine.EstadoActual;

            if (!_ctSolicitudRepo.ActualizarSolicitud(solicitud))
            {
                return StatusCode(500, "Error al actualizar el estado de la solicitud.");
            }

            // Registrar acción en AccionesEstado
            _ctAccionRepo.RegistrarAccionEstado(solicitud.IdSolicitud, estadoAnterior, solicitud.IdEstado, request.Acciones, request.Usuario);

            // Registrar evento en Eventos
            var accionEntidad = _ctAccionRepo.GetAccionEstado(solicitud.IdSolicitud, request.Acciones);
            if (accionEntidad == null)
            {
                return BadRequest("Acción no válida.");
            }

            int accionId = accionEntidad.IdAccionEstado;
            string observaciones = $"Se cambió el estado de la solicitud {solicitud.IdSolicitud} de {estadoAnterior} a {stateMachine.EstadoActual} con la acción '{request.Acciones}'.";

            _ctEventoRepo.RegistrarEvento(solicitud.IdSolicitud, accionId, estadoAnterior, stateMachine.EstadoActual, request.Usuario, observaciones);

            return Ok(solicitud);
        }


        [HttpPost]
        public IActionResult CrearSolicitud([FromBody] SolicitudDto solicitudDto)
        {
            if (solicitudDto == null)
                return BadRequest("La solicitud enviada es inválida.");

            var solicitud = _mapper.Map<Solicitud>(solicitudDto);
    
            solicitud.Creado = DateTime.Now;

            var resultado = _ctSolicitudRepo.CrearSolicitud(solicitud);
            if (!resultado)
                return StatusCode(500, "Hubo un error al guardar la solicitud.");

            var solicitudCreadaDto = _mapper.Map<SolicitudDto>(solicitud);

            return CreatedAtRoute("GetSolicitud", new { IdSolicitud = solicitud.IdSolicitud }, solicitudCreadaDto);
        }
    }
}
