using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class V004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Cidade", "CodigoPostal", "Email", "Endereco", "Estado", "Login", "Nome", "Senha", "SobreNome", "Telefone" },
                values: new object[] { new Guid("011266ab-8d3b-40ff-84c7-15736d684062"), "Almada", "2805062", "marcus.miris@gmail.com", "Rua Alvaro Vaz de Almada 3", "Setúbal", "marcus.miris", "Marcus", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "Miris", "+351910298911" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("011266ab-8d3b-40ff-84c7-15736d684062"));
        }
    }
}
