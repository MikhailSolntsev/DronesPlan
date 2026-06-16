using DronesPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DronesPlan.DAL.Configurations;

public class SnapshotBatchConfiguration : IEntityTypeConfiguration<SnapshotBatch>
{
    public void Configure(EntityTypeBuilder<SnapshotBatch> builder)
    {
        builder.ToTable("snapshot_batches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.HasIndex(x => x.CreatedAt);
    }
}
