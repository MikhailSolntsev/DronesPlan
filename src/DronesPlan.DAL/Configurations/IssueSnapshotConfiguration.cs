using DronesPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesPlan.DAL.Configurations;

public class IssueSnapshotConfiguration : IEntityTypeConfiguration<IssueSnapshot>
{
    public void Configure(EntityTypeBuilder<IssueSnapshot> builder)
    {
        builder.ToTable("issue_snapshots");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.IssueKey)
            .HasColumnName("issue_key")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Summary)
            .HasColumnName("summary")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.IssueType)
            .HasColumnName("issue_type")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.Assignee)
            .HasColumnName("assignee")
            .HasMaxLength(200);

        builder.Property(x => x.Priority)
            .HasColumnName("priority");

        builder.Property(x => x.Color)
            .HasColumnName("color")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.JiraUrl)
            .HasColumnName("jira_url")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.SnapshotBatchId)
            .HasColumnName("snapshot_batch_id");

        builder.HasOne(x => x.SnapshotBatch)
            .WithMany(x => x.Snapshots)
            .HasForeignKey(x => x.SnapshotBatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.IssueKey);
        builder.HasIndex(x => x.SnapshotBatchId);
    }
}
