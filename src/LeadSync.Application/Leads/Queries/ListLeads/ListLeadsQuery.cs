using LeadSync.Domain.Leads;

using ErrorOr;
using MediatR;

namespace LeadSync.Application.Leads.Queries.ListLeads;

public record ListLeadsQuery(LeadStatus LeadStatus) : IRequest<ErrorOr<List<Lead>>>;
