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
		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string Nombre { get; set; }
		[Required]
		public int ValorActual { get; set; }

		public virtual ICollection<Movimiento> Movimientos { get; set; }


	}
}
