using ErrorOr;
using LeadSync.Domain.Common;
using LeadSync.Domain.Contacts;

namespace LeadSync.Domain.Leads;

public class Lead : Entity
{
  public string Suburb { get; set; }

  public DateTime DateCreated { get; }

  public string Category { get; set; }

  public string Description { get; set; }

  public decimal Price { get; set; }
  public decimal FinalPrice { get; set; }

  public LeadStatus LeadStatus { get; private set; }

  public Guid ContactId { get; set; }
  public Contact Contact { get; set; } = null!;

  public Lead(
    string suburb,
    DateTime dateCreated,
    string category,
    string description,
    decimal price,
    decimal finalPrice,
    Guid contactId,
    Guid id = default
  ): base(id == default ? Guid.NewGuid() : id)
  {
    Suburb = suburb;
    DateCreated = dateCreated;
    Category = category;
    Description = description;
    Price = price;
    FinalPrice = finalPrice;
    LeadStatus = LeadStatus.New;
    ContactId = contactId;
  }

  public ErrorOr<Success> ChangeStatus(LeadStatus status)
  {
    if (status == LeadStatus.Accepted)
    {
      
    }

    LeadStatus = status;

    return Result.Success;
  }
}
