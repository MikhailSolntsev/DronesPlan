using DronesPlan.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesPlan.DAL.Configurations;

public class PlannedAssignmentConfiguration : IEntityTypeConfiguration<PlannedAssignment>
{
    public void Configure(EntityTypeBuilder<PlannedAssignment> builder)
    {
        builder.ToTable("planned_assignments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.IssueKey)
            .HasColumnName("issue_key")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(x => x.Assignee)
            .HasColumnName("assignee")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasIndex(x => new { x.IssueKey, x.Date })
            .IsUnique();

        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.Assignee);
    }
}
