using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarDance.Domain;

namespace StarDance.DAL.Configurations;

public class AbsenceConfiguration : IEntityTypeConfiguration<Absence>
{
    public void Configure(EntityTypeBuilder<Absence> builder)
    {
        builder.HasOne(absence => absence.Client)
            .WithMany(client => client.Absences);

        builder.HasOne(absence => absence.Lesson)
            .WithMany(lesson => lesson.Absences);
    }
}