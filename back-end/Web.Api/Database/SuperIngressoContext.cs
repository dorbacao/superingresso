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
                  Nome = "Administrador",
                  Senha = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"
              }
      );


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
