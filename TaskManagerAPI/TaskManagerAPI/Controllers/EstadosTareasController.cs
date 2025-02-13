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
    public class EstadosTareasController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;

        public EstadosTareasController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: api/EstadosTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosTarea>>> GetEstadosTareas()
        {
            return await _context.EstadosTareas.ToListAsync();
        }

        // GET: api/EstadosTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosTarea>> GetEstadosTarea(int id)
        {
            var estadosTarea = await _context.EstadosTareas.FindAsync(id);

            if (estadosTarea == null)
            {
                return NotFound();
            }

            return estadosTarea;
        }

        // PUT: api/EstadosTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadosTarea(int id, EstadosTarea estadosTarea)
        {
            if (id != estadosTarea.IdEstado)
            {
                return BadRequest();
            }

            _context.Entry(estadosTarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosTareaExists(id))
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

        // POST: api/EstadosTareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadosTarea>> PostEstadosTarea(EstadosTarea estadosTarea)
        {
            _context.EstadosTareas.Add(estadosTarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadosTarea", new { id = estadosTarea.IdEstado }, estadosTarea);
        }

        // DELETE: api/EstadosTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadosTarea(int id)
        {
            var estadosTarea = await _context.EstadosTareas.FindAsync(id);
            if (estadosTarea == null)
            {
                return NotFound();
            }

            _context.EstadosTareas.Remove(estadosTarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadosTareaExists(int id)
        {
            return _context.EstadosTareas.Any(e => e.IdEstado == id);
        }
    }
}
