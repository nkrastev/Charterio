namespace Charterio.Data.Configurations
{
    using global::Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> offer)
        {
            offer
                .HasOne(e => e.StartAirport)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            offer
                .HasOne(e => e.EndAirport)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
