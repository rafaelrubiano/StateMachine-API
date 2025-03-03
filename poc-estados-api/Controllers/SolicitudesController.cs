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
        private readonly IMapper _mapper;

        public SolicitudesController(IAccionRepositorio ctAccionRepo, IEstadoRepositorio ctEstadoRepo, ISolicitudRepositorio ctSolicitudRepo, IMapper mapper)
        {
            _ctAccionRepo = ctAccionRepo;
            _ctEstadoRepo = ctEstadoRepo;
            _ctSolicitudRepo = ctSolicitudRepo;
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
        public ActionResult GetSolicitudes(int IdSolicitud)
        {
            var itemSolicitud = _ctSolicitudRepo.GetSolicitudById(IdSolicitud);
            if (itemSolicitud == null)
            {
                return NotFound();
            }
            var itemSolicitudDto = _mapper.Map<SolicitudDto>(itemSolicitud);
            return Ok(itemSolicitudDto);
        }
        
        /*[HttpPost]
        public ActionResult CrearSolicitud([FromBody] CrearSolicitudDto crearSolicitudDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearSolicitudDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctSolicitudRepo.ExisteSolicitud(crearSolicitudDto.Nombre))
            {
                ModelState.AddModelError("", "El estado ya existe!");
                return StatusCode(404, ModelState);
            }
            
            var solicitud = _mapper.Map<Solicitud>(crearSolicitudDto);
            
            if (!_ctSolicitudRepo.CrearSolicitud(solicitud))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {solicitud.IdSolicitud}");
                return StatusCode(500, ModelState);
            }
            
            return CreatedAtRoute("GetSolicitud", new {EstadoId = solicitud.IdSolicitud}, solicitud);
        }*/
        
        
        
        [HttpPost("cambiarEstadoSolicitud/{id}")]
        public IActionResult CambiarEstado(int id, [FromBody] string accion)
        {
            var solicitud = _ctEstadoRepo.GetEstadoById(id);
            if (solicitud == null)
                return NotFound("Solicitud no encontrada.");

            var stateMachine = new MiStateMachine(solicitud.IdEstado, _ctAccionRepo);

            if (string.IsNullOrWhiteSpace(accion))
                return BadRequest("Acción inválida.");
            
            // 🔹 Verifica qué transiciones son posibles
            var accionesPermitidas = stateMachine.ObtenerAccionesPermitidas();
            /*Console.WriteLine($"Estado actual: {solicitud.IdEstado}");
            Console.WriteLine($"Acción recibida: {accion}");
            Console.WriteLine($"Acciones permitidas: {string.Join(", ", accionesPermitidas)}");*/

            if (!stateMachine.PuedeEjecutar(accion))
                return BadRequest("Transición no permitida.");

            stateMachine.EjecutarAccion(accion);
            solicitud.IdEstado = stateMachine.EstadoActual;

            if (!_ctEstadoRepo.ActualizarEstado(solicitud))
                return StatusCode(500, "Error al actualizar el estado.");

            return Ok(solicitud);
        }
        
        [HttpGet("{id}/estados")]
        public async Task<ActionResult<List<EstadoDto>>> GetEstadosDeSolicitud(int id)
        {
            var estados = await _context.Estados
                .Where(e => e.IdEstado == id) // Filtra correctamente según la relación
                .Select(e => new EstadoDto
                {
                    Nombre = e.Nombre,
                    Fecha = e.Creado.HasValue ? e.Creado.Value.ToString("yyyy-MM-dd") : "N/A",
                    Icono = Icons.Material.Filled.CheckCircle,
                    Color = Color.Primary
                })
                .ToListAsync();

            if (estados == null || estados.Count == 0)
            {
                return NotFound();
            }

            return estados;
        }

    }
}
