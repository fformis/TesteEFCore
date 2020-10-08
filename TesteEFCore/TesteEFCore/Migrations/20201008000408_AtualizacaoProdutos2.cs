using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteEFCore.Migrations
{
    public partial class AtualizacaoProdutos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Produtos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Produtos");
        }
    }
}
