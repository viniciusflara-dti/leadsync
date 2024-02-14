using LeadSync.Domain.Contacts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSync.Infrastructure.Contacts.Persistence;

public class ContactConfigurations : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .ValueGeneratedNever();

        builder.Property(l => l.FirstName);

        builder.Property(l => l.LastName);

        builder.Property(l => l.Email);

        builder.Property(l => l.Phone);
    }
}
