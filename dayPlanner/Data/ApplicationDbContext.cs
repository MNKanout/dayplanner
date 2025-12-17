using Microsoft.EntityFrameworkCore;
using dayPlanner.Data.Entities;

namespace dayPlanner.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets represent database tables
    public DbSet<Pupil> Pupils { get; set; }
    public DbSet<DayPlan> DayPlans { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships and constraints
        
        // DayPlan -> Pupil relationship
        modelBuilder.Entity<DayPlan>()
            .HasOne(d => d.Pupil)
            .WithMany(p => p.DayPlans)
            .HasForeignKey(d => d.PupilId)
            .OnDelete(DeleteBehavior.Cascade); // If pupil is deleted, delete their day plans

        // Activity -> DayPlan relationship
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.DayPlan)
            .WithMany(d => d.Activities)
            .HasForeignKey(a => a.DayPlanId)
            .OnDelete(DeleteBehavior.Cascade); // If day plan is deleted, delete its activities

        // Create index on DayPlan.Date for faster lookups
        modelBuilder.Entity<DayPlan>()
            .HasIndex(d => d.Date);

        // Ensure activities are ordered by Order property
        modelBuilder.Entity<Activity>()
            .HasIndex(a => new { a.DayPlanId, a.Order });
    }
}

