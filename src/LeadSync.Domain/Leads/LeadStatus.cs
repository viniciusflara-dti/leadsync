using Ardalis.SmartEnum;

namespace LeadSync.Domain.Leads;
public class LeadStatus : SmartEnum<LeadStatus>
{
  public static readonly LeadStatus New = new LeadStatus(nameof(New), 0);
  public static readonly LeadStatus Accepted = new LeadStatus(nameof(Accepted), 1);
  public static readonly LeadStatus Declined = new LeadStatus(nameof(Declined), 2);

  private LeadStatus(string name, int value) : base(name, value)
  { }
}
