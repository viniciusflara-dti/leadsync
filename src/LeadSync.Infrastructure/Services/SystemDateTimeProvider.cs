using LeadSync.Application.Common.Interfaces;

namespace LeadSync.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
