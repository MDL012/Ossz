using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniverzityProject.Model;

public partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=DataBase\\university.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("courses");

            entity.Property(e => e.Credits).HasColumnType("NUMERIC");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMERIC")
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Id)
                .HasColumnType("NUMERIC")
                .HasColumnName("ID");
            entity.Property(e => e.TeacherId)
                .HasColumnType("NUMERIC")
                .HasColumnName("TeacherID");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("department");

            entity.Property(e => e.Id)
                .HasColumnType("NUMERIC")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("student");

            entity.Property(e => e.Age).HasColumnType("NUMERIC");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMERIC")
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Enrolled).HasColumnName("Enrolled ");
            entity.Property(e => e.Id)
                .HasColumnType("NUMERIC")
                .HasColumnName("ID");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("teacher");

            entity.Property(e => e.DepartmentId)
                .HasColumnType("NUMERIC")
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Id)
                .HasColumnType("NUMERIC")
                .HasColumnName("ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
