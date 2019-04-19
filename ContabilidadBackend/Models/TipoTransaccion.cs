using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	// Ingreso_caja: se saca del banco
	public enum TipoT { Ingreso_nomina, Ingreso_caja, Ingreso_prestamo, Ingreso_extra, Prestamo, Buses, Almuerzo, Gasto_extra }
	public class TipoTransaccion
	{
		[Key]
		public int Id { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public TipoT Tipo { get; set; }
		// =======================================
		[Column(TypeName = "nvarchar(200)")]
		public string Descripcion { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public int Movimiento1Id { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public int Movimiento2Id { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public TipoMovimiento TipoMovimiento1 { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public TipoMovimiento TipoMovimiento2 { get; set; }

		public virtual ICollection<Transaccion> Transacciones { get; set; }
	}
}
