using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(user => user.Surname)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(user => user.Phone)
            .IsRequired();

        builder.Property(user => user.Login)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(user => user.Password)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(user => user.Role)
            .HasDefaultValue("client");

        builder.HasMany(user => user.Lessons)
            .WithMany(lesson => lesson.Clients)
            .UsingEntity(x => x.ToTable("ClientsLessons"));

        builder.HasMany(user => user.Queues)
            .WithOne(queue => queue.Client);

        builder.HasMany(user => user.Absences)
            .WithOne(absence => absence.Client);
    }
}