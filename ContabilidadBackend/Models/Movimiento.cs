using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	public enum TipoMovimiento { Debito, Credito}
	public class Movimiento
	{
		[Key]
		public int MovimientoId { get; set; }
		[Required]
		public TipoMovimiento Tipo { get; set; }
		/* relacion con la transaccion*/
		public int TransaccionId { get; set; }
		[ForeignKey("TransaccionId")]
		public Transaccion Transaccion { get; set; }
		/*relacion con la cuenta*/
		public int CuentaId { get; set; }
		[ForeignKey("CuentaId")]
		public Cuenta Cuenta { get; set; }


	}
}
