using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class DanceTypeConfiguration : IEntityTypeConfiguration<DanceType>
{
    public void Configure(EntityTypeBuilder<DanceType> builder)
    {
        builder.Property(dancetype => dancetype.Name)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasMany(dancetype => dancetype.Teachers)
            .WithOne(teacher => teacher.DanceType);
    }
}