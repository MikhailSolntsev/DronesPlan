using DronesPlan.Domain.Entities;
using DronesPlan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DronesPlan.DAL.Repositories;

public class SnapshotRepository : ISnapshotRepository
{
    private readonly DronesPlanDbContext _context;

    public SnapshotRepository(DronesPlanDbContext context)
    {
        _context = context;
    }

    public async Task SaveBatchAsync(SnapshotBatch batch, CancellationToken cancellationToken = default)
    {
        _context.SnapshotBatches.Add(batch);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SnapshotBatch?> GetLatestBatchAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SnapshotBatches
            .Include(b => b.Snapshots)
            .OrderByDescending(b => b.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SnapshotBatch?> GetBatchByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.SnapshotBatches
            .Include(b => b.Snapshots)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SnapshotBatch>> GetBatchesAsync(int limit, CancellationToken cancellationToken = default)
    {
        return await _context.SnapshotBatches
            .OrderByDescending(b => b.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
}
