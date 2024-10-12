using Microsoft.EntityFrameworkCore.Migrations;

namespace SnackApp.Migrations;

public partial class PopularCategorias : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("INSERT INTO Categorias(Nome, Descricao) " + "VALUES('Normal','Lanche feito com ingredientes normais')");

        migrationBuilder.Sql("INSERT INTO Categorias(Nome,Descricao) " + "VALUES('Natural','Lanche feito com ingredientes integrais e naturais')");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM Categorias");
    }
}