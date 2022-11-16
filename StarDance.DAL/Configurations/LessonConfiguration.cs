using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(lesson => lesson.DateTime)
            .IsRequired();

        builder.HasOne(lesson => lesson.Teacher)
            .WithMany(teacher => teacher.Lessons);

        builder.HasOne(lesson => lesson.Room)
            .WithMany(room => room.Lessons);
    }
}