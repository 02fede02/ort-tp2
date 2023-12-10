using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBasico.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cantidadMusicos = table.Column<int>(type: "int", nullable: false),
                    genero = table.Column<int>(type: "int", nullable: false),
                    telefonoContacto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Establecimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    mailContacto = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establecimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntradasVendidas = table.Column<int>(type: "int", nullable: true),
                    EstaAgotado = table.Column<bool>(type: "bit", nullable: false),
                    PrecioEntrada = table.Column<int>(type: "int", nullable: false),
                    BandaId = table.Column<int>(type: "int", nullable: false),
                    EstablecimientoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recital_Banda_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Banda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recital_Establecimiento_EstablecimientoId",
                        column: x => x.EstablecimientoId,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    EstablecimientoId = table.Column<int>(type: "int", nullable: false),
                    RecitalId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entrada_Establecimiento_EstablecimientoId",
                        column: x => x.EstablecimientoId,
                        principalTable: "Establecimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entrada_Recital_RecitalId",
                        column: x => x.RecitalId,
                        principalTable: "Recital",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entrada_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadEntradas = table.Column<int>(type: "int", nullable: false),
                    RecitalId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaEntradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaEntradas_Recital_RecitalId",
                        column: x => x.RecitalId,
                        principalTable: "Recital",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VentaEntradas_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Productora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Presupuesto = table.Column<int>(type: "int", nullable: false),
                    RecitalesId = table.Column<int>(type: "int", nullable: false),
                    VentaEntradasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productora_Recital_RecitalesId",
                        column: x => x.RecitalesId,
                        principalTable: "Recital",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productora_VentaEntradas_VentaEntradasId",
                        column: x => x.VentaEntradasId,
                        principalTable: "VentaEntradas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_EstablecimientoId",
                table: "Entrada",
                column: "EstablecimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_RecitalId",
                table: "Entrada",
                column: "RecitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Entrada_UsuarioId",
                table: "Entrada",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productora_RecitalesId",
                table: "Productora",
                column: "RecitalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Productora_VentaEntradasId",
                table: "Productora",
                column: "VentaEntradasId");

            migrationBuilder.CreateIndex(
                name: "IX_Recital_BandaId",
                table: "Recital",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recital_EstablecimientoId",
                table: "Recital",
                column: "EstablecimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaEntradas_RecitalId",
                table: "VentaEntradas",
                column: "RecitalId");

            migrationBuilder.CreateIndex(
                name: "IX_VentaEntradas_UsuarioId",
                table: "VentaEntradas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entrada");

            migrationBuilder.DropTable(
                name: "Productora");

            migrationBuilder.DropTable(
                name: "VentaEntradas");

            migrationBuilder.DropTable(
                name: "Recital");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Banda");

            migrationBuilder.DropTable(
                name: "Establecimiento");
        }
    }
}
