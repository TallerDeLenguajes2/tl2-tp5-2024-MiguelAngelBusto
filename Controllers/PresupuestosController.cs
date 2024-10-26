using Microsoft.AspNetCore.Mvc;
using tp.Models;
using tp.Repositorios;
using System.Collections.Generic;

namespace tp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PresupuestosController : ControllerBase
    {
        private readonly PresupuestosRepository presupuestosRepository;

        public PresupuestosController()
        {
            presupuestosRepository = new PresupuestosRepository();
        }

        [HttpPost("Agregar")]
        public ActionResult Agregar([FromBody] Presupuestos presupuesto)
        {
            if (presupuesto == null)
            {
                return BadRequest("El presupuesto no puede ser nulo.");
            }

            try
            {
                presupuestosRepository.Create(presupuesto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = presupuesto.IdPresupuesto }, presupuesto);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al agregar el presupuesto: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Presupuestos> ObtenerPorId(int id)
        {
            var presupuesto = presupuestosRepository.GetByID(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return Ok(presupuesto);
        }

        // Aquí podrías agregar más métodos para actualizar, eliminar, etc.
    }
}

