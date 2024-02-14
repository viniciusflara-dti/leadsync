using LeadSync.Application.Common.Interfaces;
using LeadSync.Domain.Leads;
using LeadSync.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;

namespace LeadSync.Infrastructure.Leads.Persistence;

public class LeadsRepository : ILeadsRepository
{
  private readonly AppDbContext _dbContext;

  public LeadsRepository(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Lead?> GetByIdAsync(Guid leadId, CancellationToken cancellationToken)
    {
        return await _dbContext.Leads.FindAsync(leadId, cancellationToken);
    }

  public async Task<List<Lead>> ListByStatusAsync(LeadStatus leadStatus, CancellationToken cancellationToken)
  {
    return await _dbContext.Leads
        .Include(l => l.Contact) // Include the Contact entity
        .Where(l => l.LeadStatus == leadStatus)
        .ToListAsync(cancellationToken);
  }

  public async Task UpdateAsync(Lead lead, CancellationToken cancellationToken)
  {
    _dbContext.Leads.Update(lead);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
}