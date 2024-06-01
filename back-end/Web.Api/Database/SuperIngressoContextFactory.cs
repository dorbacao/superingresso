using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Web.Api.Database
{
    public class SuperIngressoContextFactory : IDesignTimeDbContextFactory<SuperIngressoContext>
    {
        public SuperIngressoContext CreateDbContext(string[] args)
        {
            var connectionString = "Server=localhost;Database=super-ingresso;MultipleActiveResultSets=true;User Id=sa;Password=@Abc123#$;TrustServerCertificate=True;";
            var optionsBuilder = new DbContextOptionsBuilder<SuperIngressoContext>();
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseModel(SuperIngressoContextModel.Instance);

            return new SuperIngressoContext(optionsBuilder.Options);
        }
    }
}
