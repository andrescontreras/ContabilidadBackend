using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContabilidadBackend.Models;

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
		public DbSet<TipoTransaccion> TipoTransaccion { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movimiento>()
				.HasOne(p => p.Transaccion)
				.WithMany(b => b.Movimientos);

			modelBuilder.Entity<Cuenta>().HasData(
				new Cuenta { Id = 1, Nombre = "Banco", ValorActual = 1000000, ValorMin = 200000 },
				new Cuenta { Id = 2, Nombre = "Caja", ValorActual = 300000 },
				new Cuenta { Id = 3, Nombre = "Bus", ValorActual = 0, ValorMax = 300000 },
				new Cuenta { Id = 4, Nombre = "Almuerzo", ValorActual = 0, ValorMax = 300000 },
				new Cuenta { Id = 5,Nombre = "Prestamo", ValorActual = 0 },
				new Cuenta { Id = 6,Nombre = "GastoExtra", ValorActual = 0 },
				new Cuenta { Id = 7,Nombre = "IngresoNomina", ValorActual = 0 },
				new Cuenta { Id = 9,Nombre = "IngresoExtra", ValorActual = 0 },
				new Cuenta { Id = 10,Nombre = "Ahorro", ValorActual = 0 });

			modelBuilder.Entity<TipoTransaccion>().HasData(
				new TipoTransaccion { Id = 1, Tipo = TipoT.Ingreso_nomina, Movimiento1Id=7,Movimiento2Id=1,TipoMovimiento1=TipoMovimiento.Credito,TipoMovimiento2=TipoMovimiento.Debito},
				new TipoTransaccion { Id = 2, Tipo = TipoT.Ingreso_caja, Movimiento1Id = 1, Movimiento2Id = 2, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 3, Tipo = TipoT.Ingreso_prestamo, Movimiento1Id = 5, Movimiento2Id = 2, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 4, Tipo = TipoT.Ingreso_extra, Movimiento1Id = 9, Movimiento2Id = 2, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 5, Tipo = TipoT.Prestamo, Movimiento1Id = 2, Movimiento2Id = 5, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 6, Tipo = TipoT.Buses, Movimiento1Id = 2, Movimiento2Id = 3, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 7, Tipo = TipoT.Almuerzo, Movimiento1Id = 2, Movimiento2Id = 4, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito },
				new TipoTransaccion { Id = 8, Tipo = TipoT.Gasto_extra, Movimiento1Id = 2, Movimiento2Id = 6, TipoMovimiento1 = TipoMovimiento.Credito, TipoMovimiento2 = TipoMovimiento.Debito }
				);

			modelBuilder.Entity<Transaccion>().HasData(
				new Transaccion { Id = 1, TipoTransaccionId = 6, Valor = 50000, Fecha = DateTime.Parse("2019-04-19"), Descripcion = "pago buses mes de abril" },
				new Transaccion { Id = 2, TipoTransaccionId = 7, Valor = 30000, Fecha = DateTime.Parse("2019-04-19"), Descripcion = "pago almuerzos mes de abril" }
				);

			modelBuilder.Entity<Movimiento>().HasData(
				new Movimiento { Id = 1, Tipo = TipoMovimiento.Credito, CuentaId = 2, TransaccionId = 1 },
				new Movimiento { Id = 2, Tipo = TipoMovimiento.Credito, CuentaId = 2, TransaccionId = 2 },
				new Movimiento { Id = 3, Tipo = TipoMovimiento.Debito, CuentaId = 3, TransaccionId = 1 },
				new Movimiento { Id = 4, Tipo = TipoMovimiento.Debito, CuentaId = 4, TransaccionId = 2 }
				);


		}
	}


}
