using LeadSync.Contracts.Common;

namespace LeadSync.Contracts.Leads;
public record LeadResponse(Guid id, string Suburb, DateTime DateCreated, string Category, string Description, decimal Price, Guid ContactId);
