using LeadSync.Application.Common.Interfaces;

using ErrorOr;
using MediatR;
using LeadSync.Domain.Leads;

namespace LeadSync.Application.Leads.Commands.UpdateLeadStatus;
public class UpdateLeadStatusCommandHandler : IRequestHandler<UpdateLeadStatusCommand, ErrorOr<Success>>
{
    private readonly ILeadsRepository _leadsRepository;
    private readonly IEmailProvider _emailProvider;

    public UpdateLeadStatusCommandHandler(ILeadsRepository leadsRepository, IEmailProvider emailProvider)
    {
        _leadsRepository = leadsRepository;
        _emailProvider = emailProvider;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateLeadStatusCommand request, CancellationToken cancellationToken)
    {
        var lead = await _leadsRepository.GetByIdAsync(request.LeadId, cancellationToken);
        if (lead is null)
        {
            return Error.NotFound(description: "Lead not found");
        }

        var updatedLead = lead.ChangeStatus(request.NewStatus);

        if (updatedLead.IsError)
        {
            return updatedLead.Errors;
        }

        if (request.NewStatus == LeadStatus.Accepted)
        {
            lead.FinalPrice = lead.Price > 500 ? lead.Price * 0.9m : lead.Price;
            _emailProvider.SendEmail("vendas@test.com", "Lead Accepted", "Congratulations! Your lead has been accepted.");
        }

        await _leadsRepository.UpdateAsync(lead, cancellationToken);

        return Result.Success;
    }
}