namespace Flexybox.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  public DbSet<Department> Department { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Department>()
      .HasMany(e => e.OpenHours)
      .WithOne(e => e.Department)
      .HasForeignKey(e => e.DepartmentId);

    modelBuilder.Entity<Department>()
      .HasMany(e => e.Images);

    modelBuilder.Entity<Department>()
      .HasOne(e => e.Contact)
      .WithMany()
      .HasForeignKey(e => e.ContactId);

    modelBuilder.Entity<Department>()
      .HasOne(e => e.Address)
      .WithMany()
      .HasForeignKey(e => e.AddressId);

    modelBuilder.Entity<OpeningHours>()
      .HasMany(e => e.Days)
      .WithOne(e => e.OpeningHours)
      .HasForeignKey(e => e.OpeningHoursId);

    modelBuilder.Entity<OpeningHoursDay>()
      .HasMany(e => e.Intervals)
      .WithOne(e => e.OpeningHoursDay)
      .HasForeignKey(e => e.OpeningHoursDayId);

    modelBuilder.Entity<TimeInterval>()
      .HasOne(e => e.OpeningHoursDay)
      .WithMany(e => e.Intervals)
      .HasForeignKey(e => e.OpeningHoursDayId);

    modelBuilder.Entity<TimeInterval>()
      .Property(e => e.Start)
      .HasConversion(
        v => v,
        v => TimeSpan.Parse(v.ToString())
      );

    modelBuilder.Entity<TimeInterval>()
      .Property(e => e.End)
      .HasConversion(
        v => v,
        v => TimeSpan.Parse(v.ToString())
      );

    modelBuilder.Entity<Department>().HasData();
  }
}


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> {
  public AppDbContext CreateDbContext(string[] args) {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    optionsBuilder.UseMySql(DbConfig.GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 34)));

    return new AppDbContext(optionsBuilder.Options);
  }
}
