using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	//public enum TipoTransaccion { Pago_nomina, Pago_prestamo, Pago_extra, Buses, Almuerzo, Prestamo, Gasto_extra }
	public enum EstadoTransaccion { Correcto, Fondos_insuficientes, Restriccion}
	public class Transaccion
	{

		[Key]
		public int Id { get; set; }
		// =======================================
		[Required]
		[Column(TypeName = "INT")]
		public int Valor { get; set; }
		// =======================================
		[Column(TypeName = "DATE")]
		[Required]
		public DateTime Fecha { get; set; }
		// =======================================
		[Column(TypeName = "nvarchar(200)")]
		public string Descripcion { get; set; }
		public EstadoTransaccion? Estado { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		public int TipoTransaccionId { get; set; }
		// =======================================
		[Required]
		[ForeignKey("TipoTransaccionId")]
		public TipoTransaccion TipoTransaccion { get; set; }
		// =======================================
		public virtual ICollection<Movimiento> Movimientos { get; set; }

		public void generarMovimientos()
		{
			Console.WriteLine("---->---->---->---->---->---->");
			System.Diagnostics.Debug.WriteLine("<-----<-----<-----<-----<-----<-----<-----");

			


			//using (var context = new ContabilidadContext()) {
			//	Movimiento m = new Movimiento();
			//	m.Tipo = TipoMovimiento.Credito;
			//	m.TransaccionId = 0;

			//	context.Movimientos.Add(m);
			//	context.SaveChanges();
			//}
		}
	}
}
