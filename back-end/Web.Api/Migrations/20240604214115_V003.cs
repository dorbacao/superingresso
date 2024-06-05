using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class V003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SobreNome",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Cidade", "CodigoPostal", "Email", "Endereco", "Estado", "Login", "Nome", "Senha", "SobreNome", "Telefone" },
                values: new object[,]
                {
                    { new Guid("03ab66ab-8d3b-40ff-84c7-15736d684062"), "Almada", "2805062", "pedro.leal@gmail.com", "Rua Alvaro Vaz de Almada 3", "Setúbal", "pedro.leal", "Pedro", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "Leal", "+351910298911" },
                    { new Guid("04fd77be-8d3b-40ff-84c7-15736d684062"), "Almada", "2805062", "marcus.carreira@gmail.com", "Rua Alvaro Vaz de Almada 3", "Setúbal", "admin", "Marcus", "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", "Dorbação", "+351910298911" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("03ab66ab-8d3b-40ff-84c7-15736d684062"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("04fd77be-8d3b-40ff-84c7-15736d684062"));

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SobreNome",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "User");
        }
    }
}
