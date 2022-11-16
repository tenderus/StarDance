using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class QueueConfiguration : IEntityTypeConfiguration<Queue>
{
    public void Configure(EntityTypeBuilder<Queue> builder)
    {
        builder.Property(queue => queue.NumberOfOrder)
            .HasMaxLength(2);

        builder.HasOne(queue => queue.Client)
            .WithMany(client => client.Queues);

        builder.HasOne(queue => queue.Lesson)
            .WithMany(lesson => lesson.Queues);
    }
}