using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigurations;

internal class PasswordEntryConfiguration : IEntityTypeConfiguration<PasswordEntry>
{
    public void Configure(EntityTypeBuilder<PasswordEntry> builder)
    {
        builder.HasKey(passwordEntry => passwordEntry.Id);
        builder.HasIndex(passwordEntry => passwordEntry.Name).IsUnique();
        builder.Property(passwordEntry => passwordEntry.Password).IsRequired();
        builder.Property(passwordEntry => passwordEntry.DateAdded).IsRequired();
        builder.Property(passwordEntry => passwordEntry.IsEmail).IsRequired();
    }
}