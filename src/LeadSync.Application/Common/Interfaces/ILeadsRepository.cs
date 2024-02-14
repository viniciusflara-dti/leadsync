using LeadSync.Domain.Leads;

namespace LeadSync.Application.Common.Interfaces;

public interface ILeadsRepository
{
    Task<List<Lead>> ListByStatusAsync(LeadStatus leadStatus, CancellationToken cancellationToken);
    Task<Lead?> GetByIdAsync(Guid leadId, CancellationToken cancellationToken);
    Task UpdateAsync(Lead lead, CancellationToken cancellationToken);
}