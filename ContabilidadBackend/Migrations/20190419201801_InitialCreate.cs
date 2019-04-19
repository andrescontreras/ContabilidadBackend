using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContabilidadBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    ValorActual = table.Column<int>(type: "INT", nullable: false),
                    Mes = table.Column<int>(type: "INT", nullable: true),
                    UltimoCambio = table.Column<DateTime>(type: "DATE", nullable: true),
                    ValorMax = table.Column<int>(type: "INT", nullable: true),
                    ValorMin = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoTransaccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "INT", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Movimiento1Id = table.Column<int>(type: "INT", nullable: false),
                    Movimiento2Id = table.Column<int>(type: "INT", nullable: false),
                    TipoMovimiento1 = table.Column<int>(type: "INT", nullable: false),
                    TipoMovimiento2 = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTransaccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<int>(type: "INT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "DATE", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Estado = table.Column<int>(nullable: true),
                    TipoTransaccionId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_TipoTransaccion_TipoTransaccionId",
                        column: x => x.TipoTransaccionId,
                        principalTable: "TipoTransaccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "INT", nullable: false),
                    TransaccionId = table.Column<int>(nullable: false),
                    CuentaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_Transacciones_TransaccionId",
                        column: x => x.TransaccionId,
                        principalTable: "Transacciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cuentas",
                columns: new[] { "Id", "Mes", "Nombre", "UltimoCambio", "ValorActual", "ValorMax", "ValorMin" },
                values: new object[,]
                {
                    { 1, null, "Banco", null, 1000000, null, 200000 },
                    { 9, null, "IngresoExtra", null, 0, null, null },
                    { 7, null, "IngresoNomina", null, 0, null, null },
                    { 6, null, "GastoExtra", null, 0, null, null },
                    { 10, null, "Ahorro", null, 0, null, null },
                    { 4, null, "Almuerzo", null, 0, 300000, null },
                    { 3, null, "Bus", null, 0, 300000, null },
                    { 2, null, "Caja", null, 300000, null, null },
                    { 5, null, "Prestamo", null, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "TipoTransaccion",
                columns: new[] { "Id", "Descripcion", "Movimiento1Id", "Movimiento2Id", "Tipo", "TipoMovimiento1", "TipoMovimiento2" },
                values: new object[,]
                {
                    { 7, null, 2, 4, 6, 1, 0 },
                    { 1, null, 7, 1, 0, 1, 0 },
                    { 2, null, 1, 2, 1, 1, 0 },
                    { 3, null, 5, 2, 2, 1, 0 },
                    { 4, null, 9, 2, 3, 1, 0 },
                    { 5, null, 2, 5, 4, 1, 0 },
                    { 6, null, 2, 3, 5, 1, 0 },
                    { 8, null, 2, 6, 7, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "Id", "Descripcion", "Estado", "Fecha", "TipoTransaccionId", "Valor" },
                values: new object[] { 1, "pago buses mes de abril", null, new DateTime(2019, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 50000 });

            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "Id", "Descripcion", "Estado", "Fecha", "TipoTransaccionId", "Valor" },
                values: new object[] { 2, "pago almuerzos mes de abril", null, new DateTime(2019, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 30000 });

            migrationBuilder.InsertData(
                table: "Movimientos",
                columns: new[] { "Id", "CuentaId", "Tipo", "TransaccionId" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 3, 3, 0, 1 },
                    { 2, 2, 1, 2 },
                    { 4, 4, 0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CuentaId",
                table: "Movimientos",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_TransaccionId",
                table: "Movimientos",
                column: "TransaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_TipoTransaccionId",
                table: "Transacciones",
                column: "TipoTransaccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "TipoTransaccion");
        }
    }
}
