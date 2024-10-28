using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TecnicosRegistro.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulo",
                columns: table => new
                {
                    ArticuloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Existencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulo", x => x.ArticuloId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatssApp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Prioridad",
                columns: table => new
                {
                    PrioridadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tiempo = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridad", x => x.PrioridadId);
                });

            migrationBuilder.CreateTable(
                name: "TipoTecnico",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTecnico", x => x.TipoId);
                });

            migrationBuilder.CreateTable(
                name: "Cotizar",
                columns: table => new
                {
                    CotizacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizar", x => x.CotizacionId);
                    table.ForeignKey(
                        name: "FK_Cotizar_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tecnicos",
                columns: table => new
                {
                    TecnicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SueldoHora = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecnicos", x => x.TecnicoId);
                    table.ForeignKey(
                        name: "FK_Tecnicos_TipoTecnico_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoTecnico",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CotizarDetalles",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CotizacionId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotizarDetalles", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_CotizarDetalles_Articulo_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulo",
                        principalColumn: "ArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CotizarDetalles_Cotizar_CotizacionId",
                        column: x => x.CotizacionId,
                        principalTable: "Cotizar",
                        principalColumn: "CotizacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajo",
                columns: table => new
                {
                    TrabajoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TecnicoId = table.Column<int>(type: "int", nullable: false),
                    PrioridadId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajo", x => x.TrabajoId);
                    table.ForeignKey(
                        name: "FK_Trabajo_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajo_Prioridad_PrioridadId",
                        column: x => x.PrioridadId,
                        principalTable: "Prioridad",
                        principalColumn: "PrioridadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajo_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "TecnicoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajosDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajoId = table.Column<int>(type: "int", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajosDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_TrabajosDetalle_Articulo_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulo",
                        principalColumn: "ArticuloId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajosDetalle_Trabajo_TrabajoId",
                        column: x => x.TrabajoId,
                        principalTable: "Trabajo",
                        principalColumn: "TrabajoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articulo",
                columns: new[] { "ArticuloId", "Costo", "Descripcion", "Existencia", "Precio" },
                values: new object[,]
                {
                    { 1, 100m, "Cable tipo C", 30, 165m },
                    { 2, 300m, "Cable hdmi", 20, 500m },
                    { 3, 300m, "Memoria USB 10gb", 50, 450m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizar_ClienteId",
                table: "Cotizar",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_CotizarDetalles_ArticuloId",
                table: "CotizarDetalles",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_CotizarDetalles_CotizacionId",
                table: "CotizarDetalles",
                column: "CotizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tecnicos_TipoId",
                table: "Tecnicos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_ClienteId",
                table: "Trabajo",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_PrioridadId",
                table: "Trabajo",
                column: "PrioridadId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajo_TecnicoId",
                table: "Trabajo",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosDetalle_ArticuloId",
                table: "TrabajosDetalle",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosDetalle_TrabajoId",
                table: "TrabajosDetalle",
                column: "TrabajoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotizarDetalles");

            migrationBuilder.DropTable(
                name: "TrabajosDetalle");

            migrationBuilder.DropTable(
                name: "Cotizar");

            migrationBuilder.DropTable(
                name: "Articulo");

            migrationBuilder.DropTable(
                name: "Trabajo");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Prioridad");

            migrationBuilder.DropTable(
                name: "Tecnicos");

            migrationBuilder.DropTable(
                name: "TipoTecnico");
        }
    }
}
