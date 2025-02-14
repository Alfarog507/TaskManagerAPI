using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistorialTareasController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;

        public HistorialTareasController(TaskManagerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/HistorialTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialTarea>>> GetHistorialTareas()
        {
            try
            {
                var historialTareas = await _context.HistorialTareas.ToListAsync();
                return Ok(historialTareas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/HistorialTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialTarea>> GetHistorialTarea(int id)
        {
            try
            {
                var historialTarea = await _context.HistorialTareas.AsNoTracking().FirstOrDefaultAsync(h => h.IdHistorial == id);

                if (historialTarea == null)
                {
                    return NotFound();
                }
                return Ok(historialTarea);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/HistorialTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialTarea(int id, [FromBody] HistorialTarea historialTarea)
        {
            if (id != historialTarea.IdHistorial)
            {
                return BadRequest(new { mensaje = "El ID no coincide" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(historialTarea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Historial de tarea actualizado" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialTareaExists(id))
                {
                    return NotFound(new { mensaje = "Historial de tarea no encontrado" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el historial de tarea" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/HistorialTareas
        [HttpPost]
        public async Task<ActionResult<HistorialTarea>> PostHistorialTarea([FromBody] HistorialTarea historialTarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.HistorialTareas.Add(historialTarea);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetHistorialTarea", new { id = historialTarea.IdHistorial }, historialTarea);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/HistorialTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialTarea(int id)
        {
            try
            {
                var historialTarea = await _context.HistorialTareas.FindAsync(id);
                if (historialTarea == null)
                {
                    return NotFound(new { mensaje = "Historial de tarea no encontrado" });
                }
                _context.HistorialTareas.Remove(historialTarea);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Historial de tarea eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private bool HistorialTareaExists(int id)
        {
            return _context.HistorialTareas.Any(e => e.IdHistorial == id);
        }
    }
}

