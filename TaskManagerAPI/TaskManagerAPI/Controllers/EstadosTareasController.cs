using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosTareasController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;

        public EstadosTareasController(TaskManagerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/EstadosTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosTarea>>> GetEstadosTareas()
        {
            try
            {
                var estadosTareas = await _context.EstadosTareas.ToListAsync();
                return Ok(estadosTareas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/EstadosTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosTarea>> GetEstadosTarea(int id)
        {
            try
            {
                var estadosTarea = await _context.EstadosTareas.AsNoTracking().FirstOrDefaultAsync(e => e.IdEstado == id);

                if (estadosTarea == null)
                {
                    return NotFound();
                }
                return Ok(estadosTarea);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT: api/EstadosTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadosTarea(int id, [FromBody] EstadosTarea estadosTarea)
        {
            if (id != estadosTarea.IdEstado)
            {
                return BadRequest(new { mensaje = "El ID no coincide" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry(estadosTarea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Estado de tarea actualizado" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosTareaExists(id))
                {
                    return NotFound(new { mensaje = "Estado de tarea no encontrado" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al actualizar el estado de tarea" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/EstadosTareas
        [HttpPost]
        public async Task<ActionResult<EstadosTarea>> PostEstadosTarea([FromBody] EstadosTarea estadosTarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.EstadosTareas.Add(estadosTarea);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetEstadosTarea", new { id = estadosTarea.IdEstado }, estadosTarea);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/EstadosTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadosTarea(int id)
        {
            try
            {
                var estadosTarea = await _context.EstadosTareas.FindAsync(id);
                if (estadosTarea == null)
                {
                    return NotFound(new { mensaje = "Estado de tarea no encontrado" });
                }
                _context.EstadosTareas.Remove(estadosTarea);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Estado de tarea eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private bool EstadosTareaExists(int id)
        {
            return _context.EstadosTareas.Any(e => e.IdEstado == id);
        }
    }
}

