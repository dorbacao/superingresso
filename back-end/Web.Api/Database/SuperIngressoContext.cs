using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Web.Api.Database.Maps;
using Web.Api.Domain;

namespace Web.Api.Database
{
    public class SuperIngressoContext : DbContext
    {
        public SuperIngressoContext(DbContextOptions<SuperIngressoContext> options)
            : base(options)
        {

        }

        public ISet<User> UserSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMap());

            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = Guid.Parse("04fd77be-8d3b-40ff-84c7-15736d684062"),
                  Login = "admin",
                  Nome = "Marcus",
                  SobreNome = "Dorbação",
                  Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                  Telefone="+351910298911",
                  Email ="marcus.carreira@gmail.com",
                  Cidade = "Almada",
                  Estado = "Setúbal",
                  Endereco = "Rua Alvaro Vaz de Almada 3",
                  CodigoPostal = "2805062",
                  DataInclusao = DateTime.Now.AddDays(-4),

              },
              new User
              {
                  Id = Guid.Parse("03ab66ab-8d3b-40ff-84c7-15736d684062"),
                  Login = "pedro.leal",
                  Nome = "Pedro",
                  SobreNome = "Leal",
                  Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                  Telefone = "+351910298911",
                  Email = "pedro.leal@gmail.com",
                  Cidade = "Almada",
                  Estado = "Setúbal",
                  Endereco = "Rua Alvaro Vaz de Almada 3",
                  CodigoPostal = "2805062",
                  DataInclusao = DateTime.Now.AddDays(-4),
              },
              new User
              {
                  Id = Guid.Parse("011266ab-8d3b-40ff-84c7-15736d684062"),
                  Login = "marcus.miris",
                  Nome = "Marcus",
                  SobreNome = "Miris",
                  Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                  Telefone = "+351910298911",
                  Email = "marcus.miris@gmail.com",
                  Cidade = "Almada",
                  Estado = "Setúbal",
                  Endereco = "Rua Alvaro Vaz de Almada 3",
                  CodigoPostal = "2805062",
                  DataInclusao = DateTime.Now.AddDays(-2),
              },
              new User
              {
                  Id = Guid.Parse("021241ab-8d3b-40ff-84c7-15736d684062"),
                  Login = "luiz.cardoso",
                  Nome = "Luiz",
                  SobreNome = "Luiz",
                  Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                  Telefone = "+351910298911",
                  Email = "luiz.cardoso@gmail.com",
                  Cidade = "Almada",
                  Estado = "Setúbal",
                  Endereco = "Rua Alvaro Vaz de Almada 3",
                  CodigoPostal = "2805062",
                  DataInclusao = DateTime.Now.AddDays(-2),
              }
              );


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
