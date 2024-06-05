﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Api.Database;

#nullable disable

namespace Web.Api.Migrations
{
    [DbContext(typeof(SuperIngressoContext))]
    [Migration("20240605195325_V005")]
    partial class V005
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Web.Api.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoPostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataInclusao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SobreNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("04fd77be-8d3b-40ff-84c7-15736d684062"),
                            Ativo = false,
                            Cidade = "Almada",
                            CodigoPostal = "2805062",
                            DataInclusao = new DateTime(2024, 6, 1, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5260),
                            Email = "marcus.carreira@gmail.com",
                            Endereco = "Rua Alvaro Vaz de Almada 3",
                            Estado = "Setúbal",
                            Login = "admin",
                            Nome = "Marcus",
                            Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            SobreNome = "Dorbação",
                            Telefone = "+351910298911"
                        },
                        new
                        {
                            Id = new Guid("03ab66ab-8d3b-40ff-84c7-15736d684062"),
                            Ativo = false,
                            Cidade = "Almada",
                            CodigoPostal = "2805062",
                            DataInclusao = new DateTime(2024, 6, 1, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5315),
                            Email = "pedro.leal@gmail.com",
                            Endereco = "Rua Alvaro Vaz de Almada 3",
                            Estado = "Setúbal",
                            Login = "pedro.leal",
                            Nome = "Pedro",
                            Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            SobreNome = "Leal",
                            Telefone = "+351910298911"
                        },
                        new
                        {
                            Id = new Guid("011266ab-8d3b-40ff-84c7-15736d684062"),
                            Ativo = false,
                            Cidade = "Almada",
                            CodigoPostal = "2805062",
                            DataInclusao = new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5321),
                            Email = "marcus.miris@gmail.com",
                            Endereco = "Rua Alvaro Vaz de Almada 3",
                            Estado = "Setúbal",
                            Login = "marcus.miris",
                            Nome = "Marcus",
                            Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            SobreNome = "Miris",
                            Telefone = "+351910298911"
                        },
                        new
                        {
                            Id = new Guid("021241ab-8d3b-40ff-84c7-15736d684062"),
                            Ativo = false,
                            Cidade = "Almada",
                            CodigoPostal = "2805062",
                            DataInclusao = new DateTime(2024, 6, 3, 20, 53, 25, 393, DateTimeKind.Local).AddTicks(5326),
                            Email = "luiz.cardoso@gmail.com",
                            Endereco = "Rua Alvaro Vaz de Almada 3",
                            Estado = "Setúbal",
                            Login = "luiz.cardoso",
                            Nome = "Luiz",
                            Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            SobreNome = "Luiz",
                            Telefone = "+351910298911"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}