using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdlaBlog.WebAPI.Local.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogFicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Encabezado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloPagina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionCorta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImagenDestacada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogFicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogNodos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Encabezado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloPagina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionCorta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImagenDestacada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogNodos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogFicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BlogNodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_BlogFicas_BlogFicaId",
                        column: x => x.BlogFicaId,
                        principalTable: "BlogFicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_BlogNodos_BlogNodoId",
                        column: x => x.BlogNodoId,
                        principalTable: "BlogNodos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogFicaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BlogNodoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_BlogFicas_BlogFicaId",
                        column: x => x.BlogFicaId,
                        principalTable: "BlogFicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_BlogNodos_BlogNodoId",
                        column: x => x.BlogNodoId,
                        principalTable: "BlogNodos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogFicaId",
                table: "Comments",
                column: "BlogFicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogNodoId",
                table: "Comments",
                column: "BlogNodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogFicaId",
                table: "Tags",
                column: "BlogFicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogNodoId",
                table: "Tags",
                column: "BlogNodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BlogFicas");

            migrationBuilder.DropTable(
                name: "BlogNodos");
        }
    }
}
