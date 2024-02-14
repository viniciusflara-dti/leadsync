using System.Text.Json.Serialization;

namespace LeadSync.Contracts.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LeadStatus
{
    New,
    Accepted,
    Declined,
}
