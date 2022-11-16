using Microsoft.EntityFrameworkCore;
using StarDance.DAL.Configurations;
using StarDance.Domain;

namespace StarDance.DAL;

public class StarDanceContext : DbContext
{
    public StarDanceContext(DbContextOptions<StarDanceContext> opt) : base(opt)
    {
    }


    public DbSet<User> Clients { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<DanceType> DanceTypes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Queue> Queues { get; set; }
    public DbSet<Absence> Absences { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new DanceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new QueueConfiguration());
        modelBuilder.ApplyConfiguration(new AbsenceConfiguration());
    }
}