using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialTareasController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;

        public HistorialTareasController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialTarea>>> GetHistorialTareas()
        {
            return await _context.HistorialTareas.ToListAsync();
        }

        // GET: api/HistorialTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialTarea>> GetHistorialTarea(int id)
        {
            var historialTarea = await _context.HistorialTareas.FindAsync(id);

            if (historialTarea == null)
            {
                return NotFound();
            }

            return historialTarea;
        }

        // PUT: api/HistorialTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialTarea(int id, HistorialTarea historialTarea)
        {
            if (id != historialTarea.IdHistorial)
            {
                return BadRequest();
            }

            _context.Entry(historialTarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialTareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HistorialTareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialTarea>> PostHistorialTarea(HistorialTarea historialTarea)
        {
            _context.HistorialTareas.Add(historialTarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialTarea", new { id = historialTarea.IdHistorial }, historialTarea);
        }

        // DELETE: api/HistorialTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialTarea(int id)
        {
            var historialTarea = await _context.HistorialTareas.FindAsync(id);
            if (historialTarea == null)
            {
                return NotFound();
            }

            _context.HistorialTareas.Remove(historialTarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialTareaExists(int id)
        {
            return _context.HistorialTareas.Any(e => e.IdHistorial == id);
        }
    }
}
