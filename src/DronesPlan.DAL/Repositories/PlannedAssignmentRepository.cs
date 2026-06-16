using DronesPlan.Domain.Entities;
using DronesPlan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DronesPlan.DAL.Repositories;

public class PlannedAssignmentRepository : IPlannedAssignmentRepository
{
    private readonly DronesPlanDbContext _context;

    public PlannedAssignmentRepository(DronesPlanDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PlannedAssignment>> GetByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        return await _context.PlannedAssignments
            .Where(p => p.IssueKey == issueKey)
            .OrderBy(p => p.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PlannedAssignment plannedAssignment, CancellationToken cancellationToken = default)
    {
        _context.PlannedAssignments.Add(plannedAssignment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PlannedAssignment plannedAssignment, CancellationToken cancellationToken = default)
    {
        _context.PlannedAssignments.Remove(plannedAssignment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
