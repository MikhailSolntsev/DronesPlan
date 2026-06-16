using DronesPlan.Domain.Entities;
using DronesPlan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DronesPlan.DAL.Repositories;

public class TrackedIssueRepository : ITrackedIssueRepository
{
    private readonly DronesPlanDbContext _context;

    public TrackedIssueRepository(DronesPlanDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TrackedIssue>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TrackedIssues
            .Where(t => t.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<TrackedIssue?> GetByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        return await _context.TrackedIssues
            .FirstOrDefaultAsync(t => t.IssueKey == issueKey, cancellationToken);
    }

    public async Task AddAsync(TrackedIssue trackedIssue, CancellationToken cancellationToken = default)
    {
        _context.TrackedIssues.Add(trackedIssue);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TrackedIssue trackedIssue, CancellationToken cancellationToken = default)
    {
        _context.TrackedIssues.Update(trackedIssue);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
