using LeadSync.Domain.Leads;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSync.Infrastructure.Leads.Persistence;

public class ContactConfigurations : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .ValueGeneratedNever();

        builder.Property(l => l.Suburb);

        builder.Property(l => l.DateCreated);

        builder.Property(l => l.Category);

        builder.Property(l => l.Description);

        builder.Property(l => l.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(l => l.FinalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(l => l.LeadStatus)
            .HasConversion(
                v => v.Name,
                v => LeadStatus.FromName(v, false));

        builder.Property(l => l.ContactId);

        builder.HasOne(l => l.Contact)
                .WithOne()
                .HasForeignKey<Lead>(l => l.ContactId);
    }
}
