using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using poc_estados_api.Models;
using poc_estados_api.Models.Dtos;
using poc_estados_api.Repositorio.IRepositorio;
using MiStateMachine = poc_estados_api.StateMachine.MiStateMachine;

namespace poc_estados_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EstadosController : ControllerBase
    {
        private readonly IEstadoRepositorio _ctRepo;
        private readonly IMapper _mapper;

        public EstadosController(IEstadoRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetEstados()
        {
            var ListaEstados = _ctRepo.GetEstados();
            var listaEstadosDto = new List<EstadoDto>();
            foreach (var lista in ListaEstados)
            {
                listaEstadosDto.Add(_mapper.Map<EstadoDto>(lista));
            }
            return Ok(listaEstadosDto);
        }

        [HttpGet("IdEstado", Name = "GetEstado")]
        public ActionResult GetEstado(int IdEstado)
        {
            var itemEstado = _ctRepo.GetEstadoById(IdEstado);
            if (itemEstado == null)
            {
                return NotFound();
            }
            var itemEstadosDto = _mapper.Map<EstadoDto>(itemEstado);
            return Ok(itemEstadosDto);
        }

        [HttpPost]
        public ActionResult CrearEstado([FromBody] CrearEstadoDto crearEstadoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearEstadoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_ctRepo.ExisteEstado(crearEstadoDto.Nombre))
            {
                ModelState.AddModelError("", "El estado ya existe!");
                return StatusCode(404, ModelState);
            }
            
            var estado = _mapper.Map<Estado>(crearEstadoDto);
            
            if (!_ctRepo.CrearEstado(estado))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }
            
            return CreatedAtRoute("GetEstado", new {EstadoId = estado.IdEstado}, estado);
        }

        [HttpPatch("IdEstado", Name = "ActualizarEstado")]
        public IActionResult ActualizarPatchEstado(int IdEstado, [FromBody] EstadoDto estadoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (estadoDto == null || IdEstado != estadoDto.IdEstado)
            {
                return BadRequest(ModelState);
            }
            
            var estadoExistente = _ctRepo.GetEstadoById(IdEstado);
            if (estadoExistente == null)
            {
                return NotFound($"No se encontro la estado con ID {IdEstado}");
            }
            
            var estado = _mapper.Map<Estado>(estadoDto);
            
            if (!_ctRepo.ActualizarEstado(estado))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("IdEstado", Name = "BorrarEstado")]
        public IActionResult BorrarEstado(int IdEstado)
        {
            if (!_ctRepo.ExisteEstado(IdEstado))
            {
                return NotFound();
            }
            
            var estado = _ctRepo.GetEstadoById(IdEstado);

            if (!_ctRepo.BorrarEstado(estado))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {estado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
    }
}
