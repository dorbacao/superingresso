using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class V007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("011266ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 4, 15, 29, 7, 70, DateTimeKind.Local).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("021241ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 4, 15, 29, 7, 70, DateTimeKind.Local).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("03ab66ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 2, 15, 29, 7, 70, DateTimeKind.Local).AddTicks(3701));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("04fd77be-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 2, 15, 29, 7, 70, DateTimeKind.Local).AddTicks(3647));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoPostal",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("011266ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5321));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("021241ab-8d3b-40ff-84c7-15736d684062"),
                column: "DataInclusao",
                value: new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5326));

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
        }
    }
}
