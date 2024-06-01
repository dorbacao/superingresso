using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Domain;

namespace Web.Api.Database.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).IsRequired();
            builder.Property(a => a.Login).IsRequired();
            builder.Property(a => a.Senha).IsRequired();

            builder.ToTable("User");
        }
    }
}
