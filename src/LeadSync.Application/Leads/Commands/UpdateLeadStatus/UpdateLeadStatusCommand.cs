using ErrorOr;
using MediatR;
using LeadSync.Domain.Leads;

namespace LeadSync.Application.Leads.Commands.UpdateLeadStatus
{
    public record UpdateLeadStatusCommand(Guid LeadId, LeadStatus NewStatus) : IRequest<ErrorOr<Success>>;
}