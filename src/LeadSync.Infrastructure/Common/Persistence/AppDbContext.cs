using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using LeadSync.Domain.Leads;
using LeadSync.Domain.Contacts;
using LeadSync.Domain.Common;
using LeadSync.Infrastructure.Common.Middleware;
namespace LeadSync.Infrastructure.Common;

public class AppDbContext : DbContext
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IPublisher _publisher;

  public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, IPublisher publisher) : base(options)
  {
    _httpContextAccessor = httpContextAccessor;
    _publisher = publisher;
  }

  public DbSet<Lead> Leads { get; set; } = null!;
  public DbSet<Contact> Contacts { get; set; } = null!;

  public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var domainEvents = ChangeTracker.Entries<Entity>()
      .SelectMany(entry => entry.Entity.PopDomainEvents())
      .ToList();

    if (IsUserWaitingOnline())
    {
      AddDomainEventsToOfflineProcessingQueue(domainEvents);
      return await base.SaveChangesAsync(cancellationToken);
    }

    await PublishDomainEvents(domainEvents);
    return await base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    var contact1Id = Guid.NewGuid();
    var contact2Id = Guid.NewGuid();
    var contact3Id = Guid.NewGuid();
    var contact4Id = Guid.NewGuid();

    var contacts = new List<Contact>
    {
        new Contact("Bill", "Gates", "billgates@gmail.com", "1234567890", id: contact1Id),
        new Contact("Steve", "Jobs", "stevejobs@apple.com", "1234567890", id: contact2Id),
        new Contact("Elon", "Musk", "elonmusk@spacex.com", "1234567890", id: contact3Id),
        new Contact("Jeff", "Bezos", "jeffbezos@amazon.com", "1234567890", id: contact4Id)
    };

    var leads = new List<Lead>
    {
        new Lead("Yanderra 2574", DateTime.UtcNow, "Painters", "Need to paint 2 aluminum windows and a sliding glass door", 62, 0, contact1Id),
        new Lead("Woolooware 2230", DateTime.UtcNow, "Interior Painters", "Internal walls 3 colors", 600, 0, contact2Id),
        new Lead("Carramar 6031", DateTime.UtcNow, "General Building Work", "Plaster exposed brick walls (see photos), square off 2 archways (see photos), and expand pantry (see photos).", 200, 0, contact3Id),
        new Lead("Quinns Rocks 6030", DateTime.UtcNow, "Home Renovations", "There is a two story building at the front of the main house that's about 10x5 that would like to convert into self contained living area.", 300, 0, contact4Id)
    };

    modelBuilder.Entity<Lead>().HasData(leads);
    modelBuilder.Entity<Contact>().HasData(contacts);

    base.OnModelCreating(modelBuilder);
  }

  private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

  private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
  {
    foreach (var domainEvent in domainEvents)
    {
      await _publisher.Publish(domainEvent);
    }
  }

  private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
  {
    Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext!.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
      value is Queue<IDomainEvent> existingDomainEvents
        ? existingDomainEvents
        : new();

    domainEvents.ForEach(domainEventsQueue.Enqueue);
    _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
  }
}