using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivroCurso.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoacolunadataDevolução : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEmprestimo",
                table: "Emprestimos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEmprestimo",
                table: "Emprestimos");
        }
    }
}
