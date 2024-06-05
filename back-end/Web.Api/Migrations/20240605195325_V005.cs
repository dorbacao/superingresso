using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class V005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("011266ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("03ab66ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 1, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5315));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("04fd77be-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 1, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5260));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Cidade", "CodigoPostal", "DataInclusao", "Email", "Endereco", "Estado", "Login", "Nome", "Senha", "SobreNome", "Telefone" },
                values: new object[] { new Guid("021241ab-8d3b-40ff-84c7-15736d684062"), "Almada", "2805062", new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5326), "luiz.cardoso@gmail.com", "Rua Alvaro Vaz de Almada 3", "Setúbal", "luiz.cardoso", "Luiz", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "Luiz", "+351910298911" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("021241ab-8d3b-40ff-84c7-15736d684062"));

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "User");
        }
    }
}
