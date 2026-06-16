using DronesPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DronesPlan.DAL;

public class DronesPlanDbContext : DbContext
{
    public DronesPlanDbContext(DbContextOptions<DronesPlanDbContext> options)
        : base(options)
    {
    }

    public DbSet<IssueSnapshot> IssueSnapshots => Set<IssueSnapshot>();
    public DbSet<SnapshotBatch> SnapshotBatches => Set<SnapshotBatch>();
    public DbSet<TrackedIssue> TrackedIssues => Set<TrackedIssue>();
    public DbSet<PlannedAssignment> PlannedAssignments => Set<PlannedAssignment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DronesPlanDbContext).Assembly);
    }
}
