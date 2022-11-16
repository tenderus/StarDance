using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.Property(room => room.Number)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(room => room.Capacity)
            .HasMaxLength(2)
            .IsRequired();

        builder.HasMany(room => room.Lessons)
            .WithOne(lesson => lesson.Room);
    }
}