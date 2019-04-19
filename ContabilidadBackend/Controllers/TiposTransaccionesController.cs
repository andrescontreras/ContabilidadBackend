using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContabilidadBackend.Models;

namespace ContabilidadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposTransaccionesController : ControllerBase
    {
        private readonly ContabilidadContext _context;

        public TiposTransaccionesController(ContabilidadContext context)
        {
            _context = context;
        }

        // GET: api/TiposTransacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTransaccion>>> GetTipoTransaccion()
        {
            return await _context.TipoTransaccion.ToListAsync();
        }

        // GET: api/TiposTransacciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTransaccion>> GetTipoTransaccion(int id)
        {
            var tipoTransaccion = await _context.TipoTransaccion.FindAsync(id);

            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            return tipoTransaccion;
        }

        // PUT: api/TiposTransacciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTransaccion(int id, TipoTransaccion tipoTransaccion)
        {
            if (id != tipoTransaccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoTransaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoTransaccionExists(id))
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

        // POST: api/TiposTransacciones
        [HttpPost]
        public async Task<ActionResult<TipoTransaccion>> PostTipoTransaccion(TipoTransaccion tipoTransaccion)
        {
            _context.TipoTransaccion.Add(tipoTransaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoTransaccion", new { id = tipoTransaccion.Id }, tipoTransaccion);
        }

        // DELETE: api/TiposTransacciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoTransaccion>> DeleteTipoTransaccion(int id)
        {
            var tipoTransaccion = await _context.TipoTransaccion.FindAsync(id);
            if (tipoTransaccion == null)
            {
                return NotFound();
            }

            _context.TipoTransaccion.Remove(tipoTransaccion);
            await _context.SaveChangesAsync();

            return tipoTransaccion;
        }

        private bool TipoTransaccionExists(int id)
        {
            return _context.TipoTransaccion.Any(e => e.Id == id);
        }
    }
}
