using ContabilidadBackend.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Commands
{
	public class CrearTransaccion
	{
		private readonly ContabilidadContext _context;
		private Transaccion transaccion;
		private Cuenta cuentaCredito;
		private Cuenta cuentaDebito;
		public CrearTransaccion(ContabilidadContext context)
		{
			_context = context;
		}
		public async Task<Transaccion> crearTransaccion(Transaccion transaccion)
		{

			this.transaccion = transaccion;
			this.transaccion.Estado = EstadoTransaccion.Sin_confirmar;
			var t = _context.Transacciones.Add(this.transaccion);
			this.transaccion.Id = t.Entity.Id;
			await _context.SaveChangesAsync();
			generarMovimientos();
			return transaccion;


		}

		public void generarMovimientos()
		{
			System.Diagnostics.Debug.WriteLine("<-----<-----<-----<-----<-----<-----<-----");
			var tipoTransaccion = GetTipoTransaccion();
			afectarCuentas(tipoTransaccion);

			if (transaccion.Estado == EstadoTransaccion.Correcto)
			{
				Movimiento m1 = new Movimiento();
				m1.Tipo = tipoTransaccion.TipoMovimiento1;
				m1.CuentaId = tipoTransaccion.Movimiento1Id;
				m1.TransaccionId = this.transaccion.Id;



				Movimiento m2 = new Movimiento();
				m2.Tipo = tipoTransaccion.TipoMovimiento2;
				m2.CuentaId = tipoTransaccion.Movimiento2Id;
				m2.TransaccionId = transaccion.Id;

				_context.Movimientos.Add(m1);
				_context.Movimientos.Add(m2);

				_context.Cuentas.Update(cuentaCredito);
				_context.Cuentas.Update(cuentaDebito);

				_context.Transacciones.Update(transaccion);
				_context.SaveChanges();
			}
			else
			{
				_context.Transacciones.Update(transaccion);
				_context.SaveChanges();
			}
		}

		public void afectarCuentas(TipoTransaccion tipoTransaccion)
		{
			
			if (tipoTransaccion.TipoMovimiento1 == TipoMovimiento.Debito)
			{
				cuentaDebito = getCuenta(tipoTransaccion.Movimiento1Id);
				cuentaCredito = getCuenta(tipoTransaccion.Movimiento2Id);
			}
			else
			{
				cuentaDebito = getCuenta(tipoTransaccion.Movimiento2Id);
				cuentaCredito = getCuenta(tipoTransaccion.Movimiento1Id);
			}

			EstadoTransaccion estadoCuentaCredito = manejoCuentaCredito();
			EstadoTransaccion estadoCuentaDebito = manejoCuentaDebito();

			if (estadoCuentaCredito == EstadoTransaccion.Correcto && estadoCuentaDebito == EstadoTransaccion.Correcto)
			{
				transaccion.Estado = EstadoTransaccion.Correcto;
			}
			else
			{
				transaccion.Estado = (estadoCuentaCredito == EstadoTransaccion.Correcto ? estadoCuentaDebito : estadoCuentaCredito);  
			}


		}

		public TipoTransaccion GetTipoTransaccion()
		{

			var tipoTransaccion = _context.TipoTransaccion
				.SingleOrDefault(t => t.Id == transaccion.TipoTransaccionId);
			return tipoTransaccion;


		}

		public Cuenta getCuenta(int cuentaId)
		{
			var Cuenta = _context.Cuentas
			   .SingleOrDefault(c => c.Id == cuentaId);
			return Cuenta;
		}

		public EstadoTransaccion manejoCuentaDebito()
		{
			var valorResultante = cuentaDebito.ValorActual + transaccion.Valor;
			if (valorResultante < cuentaDebito.ValorMax || cuentaDebito.ValorMax == null)
			{
				cuentaDebito.ValorActual = valorResultante;
				cuentaDebito.UltimoCambio = transaccion.Fecha;
				return EstadoTransaccion.Correcto;
			}
			else
			{
				return EstadoTransaccion.Restriccion;
			}
		}

		public EstadoTransaccion manejoCuentaCredito()
		{
			var valorResultante = cuentaCredito.ValorActual - transaccion.Valor;
			if (valorResultante > 0)
			{
				if (valorResultante > cuentaCredito.ValorMin || cuentaCredito.ValorMin == null )
				{
					cuentaCredito.ValorActual = valorResultante;
					cuentaCredito.UltimoCambio = transaccion.Fecha;
					return EstadoTransaccion.Correcto;
				}
				else
				{
					return EstadoTransaccion.Restriccion;
				}
			}
			else
			{
				return EstadoTransaccion.Fondos_insuficientes;
			}

		}
	}
}
