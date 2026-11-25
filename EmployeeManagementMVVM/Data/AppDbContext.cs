using EmployeeManagementMVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementMVVM.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(80).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(80).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(160).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Department).HasMaxLength(80).IsRequired();
            entity.Property(e => e.City).HasMaxLength(80);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Skills).HasMaxLength(500);
        });
    }
}
