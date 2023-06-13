using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class StudentContext : DbContext
{
    public DbSet<Student> students { get; set; }
    public DbSet<GradeInCourse> grades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Student.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(student => student.id);
        modelBuilder.Entity<GradeInCourse>().HasKey(grade => grade.id);
    }
}