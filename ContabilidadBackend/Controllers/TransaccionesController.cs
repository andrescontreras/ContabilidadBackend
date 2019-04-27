using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContabilidadBackend.Models;
using ContabilidadBackend.Commands;

namespace ContabilidadBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ContabilidadContext _context;

        public TransaccionesController(ContabilidadContext context)
        {
            _context = context;
        }

        // GET: api/Transacciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransacciones()
        {
			Transaccion t = new Transaccion();
			//t.generarMovimientos();

			var transacciones = await _context.Transacciones
				.Include(transaccion => transaccion.Movimientos)
				.ToListAsync();
			return transacciones;
        }

        // GET: api/Transacciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);

            if (transaccion == null)
            {
                return NotFound();
            }

            return transaccion;
        }

        // PUT: api/Transacciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaccion(int id, Transaccion transaccion)
        {
            if (id != transaccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaccionExists(id))
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

        // POST: api/Transacciones
        [HttpPost]
        public async Task<ActionResult<Transaccion>> PostTransaccion(Transaccion transaccion)
        {
			var c = new CrearTransaccion(_context);
			var t =  await c.crearTransaccion(transaccion);
			//transaccion.generarEstado();
			//         _context.Transacciones.Add(transaccion);
			//         await _context.SaveChangesAsync();
			//transaccion.generarMovimientos(transaccion);


			//System.Diagnostics.Debug.WriteLine("<-----<-----<-----<-----<-----<-----<-----");
			//System.Diagnostics.Debug.WriteLine(t.Entity);
			//return Ok();
			return CreatedAtAction("GetTransaccion", new { id = t.Id }, t);
			
		}

        // DELETE: api/Transacciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Transaccion>> DeleteTransaccion(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();

            return transaccion;
        }

        private bool TransaccionExists(int id)
        {
            return _context.Transacciones.Any(e => e.Id == id);
        }
    }
}
