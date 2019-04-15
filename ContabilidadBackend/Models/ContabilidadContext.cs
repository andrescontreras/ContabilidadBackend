using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadBackend.Models
{
	public class ContabilidadContext : DbContext
	{
		public ContabilidadContext()
		{
		}

		public ContabilidadContext(DbContextOptions<ContabilidadContext> options) : base(options)
		{

		}
		public DbSet<Transaccion> Transacciones { get; set; }
		public DbSet<Movimiento> Movimientos { get; set; }
		public DbSet<Cuenta> Cuentas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movimiento>()
				.HasOne(p => p.Transaccion)
				.WithMany(b => b.Movimientos);


		}

	
	}


}
