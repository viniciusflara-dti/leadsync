namespace LeadSync.Contracts.Common;

public interface IContact
{
  Guid id { get; }
  string FirstName { get; }
  string LastName { get; }
  string Email { get; }
  string PhoneNumber { get; }
}

