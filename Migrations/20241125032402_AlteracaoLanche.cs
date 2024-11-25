using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnackApp.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoLanche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanchePreferido",
                table: "Lanches",
                newName: "Preferido");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preferido",
                table: "Lanches",
                newName: "LanchePreferido");
        }
    }
}
