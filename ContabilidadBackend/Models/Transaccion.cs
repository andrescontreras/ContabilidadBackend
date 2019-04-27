using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContabilidadBackend.Models
{
	//public enum TipoTransaccion { Pago_nomina, Pago_prestamo, Pago_extra, Buses, Almuerzo, Prestamo, Gasto_extra }
	public enum EstadoTransaccion { Correcto, Fondos_insuficientes, Restriccion, Sin_confirmar }
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
		[Required]
		public int TipoTransaccionId { get; set; }
		// =======================================
		[ForeignKey("TipoTransaccionId")]
		public TipoTransaccion tipoTransaccion { get; set; }
		// =======================================
		public virtual ICollection<Movimiento> Movimientos { get; set; }

	}
}
