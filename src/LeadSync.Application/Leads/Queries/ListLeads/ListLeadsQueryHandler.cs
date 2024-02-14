using LeadSync.Application.Common.Interfaces;
using LeadSync.Domain.Leads;

using ErrorOr;

using MediatR;

namespace LeadSync.Application.Leads.Queries.ListLeads;

public class ListLeadsQueryHandler : IRequestHandler<ListLeadsQuery, ErrorOr<List<Lead>>>
{
    private readonly ILeadsRepository _leadsRepository;

    public ListLeadsQueryHandler(ILeadsRepository leadsRepository)
    {
        _leadsRepository = leadsRepository;
    }

    public async Task<ErrorOr<List<Lead>>> Handle(ListLeadsQuery request, CancellationToken cancellationToken)
    {
        return await _leadsRepository.ListByStatusAsync(request.LeadStatus, cancellationToken);
    }
}
