using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTechBank.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addColumnIdUsuarioInTblCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Cliente",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Cliente");
        }
    }
}
