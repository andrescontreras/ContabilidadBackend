using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	public enum TipoTransaccion { Pago_nomina, Pago_prestamo, Pago_extra, Buses, Almuerzo, Prestamo, Gasto_extra }
	public class Transaccion
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public TipoTransaccion Tipo { get; set; }
		[Required]
		public int Valor { get; set; }
		[Required]
		public DateTime Fecha { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Descripcion { get; set; }

		public virtual ICollection<Movimiento> Movimientos { get; set; }
	}
}
