using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Models;

namespace DziennikSportowca.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<TrainingPlanExercise>()
                .HasKey(t => new { t.ExerciseId, t.TrainingPlanId });

            builder.Entity<TrainingPlanExercise>()
                .HasOne(t => t.TrainingPlan)
                .WithMany(t => t.Exercises)
                .HasForeignKey(t => t.TrainingPlanId);

            builder.Entity<TrainingPlanExercise>()
                .HasOne(t => t.Exercise)
                .WithMany(t => t.TrainingPlans)
                .HasForeignKey(t => t.ExerciseId);

            builder.Entity<MusclePartExercise>()
                .HasKey(m => new { m.ExerciseId, m.MuscePartId });

            builder.Entity<MusclePartExercise>()
                .HasOne(m => m.MusclePart)
                .WithMany(m => m.Exercises)
                .HasForeignKey(m => m.MuscePartId);

            builder.Entity<MusclePartExercise>()
                .HasOne(m => m.Exercise)
                .WithMany(m => m.MuscleParts)
                .HasForeignKey(m => m.ExerciseId);

            builder.Entity<TrainingPlan>().ToTable("TrainingPlan");
            builder.Entity<Exercise>().ToTable("Exercise");
            builder.Entity<TrainingPlanExercise>().ToTable("TrainingPlanExercise");
            builder.Entity<MusclePart>().ToTable("MusclePart");
            builder.Entity<MusclePartExercise>().ToTable("MusclePartExercise");
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingPlan> TrainingPlans { get; set; }
        public DbSet<TrainingPlanExercise> TrainingPlanExercises { get; set; }
        public DbSet<MusclePart> MuscleParts { get; set; }
        public DbSet<MusclePartExercise> MusclePartExercises { get; set; }
    }
}
