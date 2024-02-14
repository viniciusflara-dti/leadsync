using LeadSync.Application.Leads.Queries.ListLeads;
using LeadSync.Application.Leads.Commands.UpdateLeadStatus;
using LeadSync.Contracts.Leads;
using LeadSync.Domain.Leads;

using MediatR;
using Microsoft.AspNetCore.Mvc;

using DomainLeadStatus = LeadSync.Domain.Leads.LeadStatus;
using ContractsLeadStatus = LeadSync.Contracts.Common.LeadStatus;

namespace LeadSync.Api.Controllers;

[Route("api/leads")]
public class LeadsController : ApiController
{
  private readonly ISender _mediator;

  public LeadsController(ISender mediator)
  {
    _mediator = mediator;
  }

  [HttpGet("invited")]
  public async Task<ActionResult<IEnumerable<string>>> ListInvitedLeads()
  {
    var query = new ListLeadsQuery(LeadStatus.New);

    var result = await _mediator.Send(query);

    return result.Match(
            leads => Ok(leads),
            Problem);
  }

  [HttpGet("accepted")]
  public async Task<ActionResult<IEnumerable<string>>> ListAcceptedLeads()
  {
    var query = new ListLeadsQuery(LeadStatus.Accepted);

    var result = await _mediator.Send(query);

    return result.Match(
            leads => Ok(leads),
            Problem);
  }

  [HttpPut("{leadId:guid}")]
  public async Task<IActionResult> UpdateLeadStatus(Guid leadId, UpdateLeadStatusRequest request)
  {
    var command = new UpdateLeadStatusCommand(leadId, ToDto(request.LeadStatus));

    var result = await _mediator.Send(command);

    return result.Match(
        _ => NoContent(),
        Problem);
  }

  private static DomainLeadStatus ToDto(ContractsLeadStatus leadStatus) =>
    leadStatus switch
    {
      ContractsLeadStatus.New => DomainLeadStatus.New,
      ContractsLeadStatus.Accepted => DomainLeadStatus.Accepted,
      ContractsLeadStatus.Declined => DomainLeadStatus.Declined,
      _ => throw new InvalidOperationException(),
    };
}
