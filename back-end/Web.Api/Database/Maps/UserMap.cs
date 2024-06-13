using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Domain.IdentityAgg;

namespace Web.Api.Database.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).IsRequired();
            builder.Property(a => a.SobreNome).IsRequired(false);
            builder.Property(a => a.Login).IsRequired();
            builder.Property(a => a.Senha).IsRequired(false);
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Endereco).IsRequired(false);
            builder.Property(a => a.Telefone).IsRequired(false);
            builder.Property(a => a.Cidade).IsRequired(false);
            builder.Property(a => a.Estado).IsRequired(false);
            builder.Property(a => a.CodigoPostal).IsRequired(false);
            builder.Property(a => a.Ativo).IsRequired().HasDefaultValue(true);

            builder.ToTable("User");
        }
    }
}
