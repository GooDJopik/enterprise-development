using CarRental.Domain;
using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.EfCore;

/// <summary>
/// EF Core database context for the car rental domain.
/// Configured to be used with PostgreSQL.
/// </summary>
/// <param name="options">Context options.</param>
public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Cars available for rental.
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Clients who can rent cars.
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Car models.
    /// </summary>
    public DbSet<Model> Models => Set<Model>();

    /// <summary>
    /// Model generations.
    /// </summary>
    public DbSet<ModelGeneration> ModelGenerations => Set<ModelGeneration>();

    /// <summary>
    /// Rental transactions.
    /// </summary>
    public DbSet<Rental> Rentals => Set<Rental>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Model>(entity =>
        {
            entity.ToTable("models");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            entity.Property(x => x.DriveType)
                .HasColumnName("drive_type")
                .IsRequired();

            entity.Property(x => x.Seats)
                .HasColumnName("seats");

            entity.Property(x => x.BodyType)
                .HasColumnName("body_type")
                .IsRequired();

            entity.Property(x => x.CarClass)
                .HasColumnName("car_class")
                .IsRequired();
        });

        modelBuilder.Entity<ModelGeneration>(entity =>
        {
            entity.ToTable("model_generations");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.ModelId)
                .HasColumnName("model_id");

            entity.Property(x => x.Year)
                .HasColumnName("year");

            entity.Property(x => x.EngineVolume)
                .HasColumnName("engine_volume");

            entity.Property(x => x.TransmissionType)
                .HasColumnName("transmission_type")
                .IsRequired();

            entity.Property(x => x.PricePerHour)
                .HasColumnName("price_per_hour")
                .HasColumnType("numeric(18,2)");

            entity.HasOne(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId)
                .IsRequired();
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("cars");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.LicensePlate)
                .HasColumnName("license_plate")
                .IsRequired();

            entity.Property(x => x.Color)
                .HasColumnName("color")
                .IsRequired();

            entity.Property(x => x.GenerationId)
                .HasColumnName("generation_id");

            entity.HasOne(x => x.Generation)
                .WithMany()
                .HasForeignKey(x => x.GenerationId)
                .IsRequired();
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.LicenseNumber)
                .HasColumnName("license_number")
                .IsRequired();

            entity.Property(x => x.FullName)
                .HasColumnName("full_name")
                .IsRequired();

            entity.Property(x => x.BirthDate)
                .HasColumnName("birth_date")
                .HasColumnType("date");
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.ToTable("rentals");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.ClientId)
                .HasColumnName("client_id");

            entity.Property(x => x.CarId)
                .HasColumnName("car_id");

            entity.Property(x => x.StartTime)
                .HasColumnName("start_time");

            entity.Property(x => x.DurationHours)
                .HasColumnName("duration_hours");

            entity.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .IsRequired();

            entity.HasOne(x => x.Car)
                .WithMany()
                .HasForeignKey(x => x.CarId)
                .IsRequired();
        });

        var dataSeeder = new DataSeeder();

        modelBuilder.Entity<Model>().HasData(
            dataSeeder.Models.Select(m => new
            {
                m.Id,
                m.Name,
                m.DriveType,
                m.Seats,
                m.BodyType,
                m.CarClass
            }));

        modelBuilder.Entity<ModelGeneration>().HasData(
            dataSeeder.Generations.Select(g => new
            {
                g.Id,
                ModelId = g.Model.Id,
                g.Year,
                g.EngineVolume,
                g.TransmissionType,
                g.PricePerHour
            }));

        modelBuilder.Entity<Car>().HasData(
            dataSeeder.Cars.Select(c => new
            {
                c.Id,
                c.LicensePlate,
                c.Color,
                GenerationId = c.Generation.Id
            }));

        modelBuilder.Entity<Client>().HasData(
            dataSeeder.Clients.Select(c => new
            {
                c.Id,
                c.LicenseNumber,
                c.FullName,
                c.BirthDate
            }));

        modelBuilder.Entity<Rental>().HasData(
            dataSeeder.Rentals.Select(r => new
            {
                r.Id,
                ClientId = r.Client.Id,
                CarId = r.Car.Id,
                r.StartTime,
                r.DurationHours
            }));
    }
}