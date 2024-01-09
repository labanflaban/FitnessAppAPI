using System;
using System.Collections.Generic;
using FittnessAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FittnessAppAPI.Data;

public partial class FitnessDbContext : DbContext
{
    public FitnessDbContext()
    {
    }

    public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExcerciseDay> ExcerciseDays { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<MuscleGroup> MuscleGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FitnessDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExcerciseDay>(entity =>
        {
            entity.ToTable("excerciseDays");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.ToTable("exercises");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.MuscleGroupId).HasColumnName("muscleGroupId");

        });

        modelBuilder.Entity<MuscleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_muscleGroups");
            entity.Property(e => e.Id).HasColumnName("Id");

            entity.ToTable("muscleGroups");

            entity.Property(e => e.Name).HasColumnName("muscleGroup");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
