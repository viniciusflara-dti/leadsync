using LeadSync.Domain.Common;
namespace LeadSync.Domain.Contacts;
public class Contact: Entity
{
  public string FirstName { get; set; }

  public string LastName { get; set; } = null!;

  public string Email { get; set; } = null!;

  public string Phone { get; set; } = null!;

  public Contact(
    string firstName,
    string lastName,
    string email,
    string phone,
    Guid id = default
  ): base(id == default ? Guid.NewGuid() : id)
  {
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    Phone = phone;
  }
}