using Microsoft.EntityFrameworkCore;
using MonitorSensors.Entities;
using Type = MonitorSensors.Entities.Type;

namespace MonitorSensors.Data;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<Uint> Uints { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sensor>().ToTable("Sensor");
        modelBuilder.Entity<Type>().ToTable("Type");
        modelBuilder.Entity<Uint>().ToTable("Uint");


        modelBuilder.Entity<Type>().HasData(
            new Type { Id = 1, Name = "Pressure" },
            new Type { Id = 2, Name = "Voltage" },
            new Type { Id = 3, Name = "Temperature", },
            new Type { Id = 4, Name = "Humidity", }
        );

        modelBuilder.Entity<Uint>().HasData(
            new Uint { Id = 1, Name = "bar" },
            new Uint { Id = 2, Name = "voltage" },
            new Uint { Id = 3, Name = "\u00b0C", },
            new Uint { Id = 4, Name = "%", }
        );
    }
}