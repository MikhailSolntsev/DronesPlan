using DronesPlan.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesPlan.DAL.Configurations;

public class TrackedIssueConfiguration : IEntityTypeConfiguration<TrackedIssue>
{
    public void Configure(EntityTypeBuilder<TrackedIssue> builder)
    {
        builder.ToTable("tracked_issues");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.IssueKey)
            .HasColumnName("issue_key")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.AddedAt)
            .HasColumnName("added_at")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(x => x.ManualPriority)
            .HasColumnName("manual_priority");

        builder.Property(x => x.Notes)
            .HasColumnName("notes")
            .HasMaxLength(1000);

        builder.HasIndex(x => x.IssueKey)
            .IsUnique();

        builder.HasIndex(x => x.IsActive);
    }
}
