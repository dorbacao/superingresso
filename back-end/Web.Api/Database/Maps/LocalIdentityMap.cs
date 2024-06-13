using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Domain.IdentityAgg;

namespace Web.Api.Database.Maps
{
    public class LocalIdentityMap : IEntityTypeConfiguration<LocalIdentity>
    {
        public void Configure(EntityTypeBuilder<LocalIdentity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasAlternateKey(a => new { a.ProviderSubject, a.LoginProvider });
            builder.Property(a=>a.PictureUrl);
            builder.Property(a=>a.EmailOrLogin).IsRequired();
            builder.Property(a=>a.Password).IsRequired(false);
            builder.Property(a=>a.SurName).IsRequired();
            builder.Property(a=>a.GivenName).IsRequired();
            builder.HasOne(a => a.User)
                .WithMany(a=>a.Identities)
                .HasForeignKey(a => a.UserId);

        }
    }
}
