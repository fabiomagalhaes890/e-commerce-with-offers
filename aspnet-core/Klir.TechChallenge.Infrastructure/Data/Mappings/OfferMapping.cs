using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Klir.TechChallenge.Infrastructure.Data.Mappings
{
    public sealed class OfferMapping : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(u => u.ProductId).IsRequired();
            builder.Property(u => u.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(u => u.Quantity).IsRequired();
            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Status).IsRequired();
        }
    }
}
