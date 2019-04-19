using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	public class Cuenta
	{
		[Key]
		public int Id { get; set; }
		// =======================================
		[Column(TypeName = "NVARCHAR(50)")]
		[Required]
		public string Nombre { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		[Required]
		public int ValorActual { get; set; }
		// =======================================
		[Column(TypeName ="INT")]
		public int? Mes { get; set; }
		// =======================================
		[Column(TypeName ="DATE")]
		public DateTime? UltimoCambio { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		public int? ValorMax { get; set; }
		// =======================================
		[Column(TypeName = "INT")]
		public int? ValorMin { get; set; }
		// =======================================
		public virtual ICollection<Movimiento> Movimientos { get; set; }


	}
}
