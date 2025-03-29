using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroClientesSS.Migrations
{
    public partial class InitialCreate : Migration
    {    
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CLIENTES",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nrDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nrTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    flExcluido = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENTES", x => x.idCliente);
                });
        }
       
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CLIENTES");
        }
    }
}
