using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Data.Entities;

public class Tiers
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan TimeToCompleteRequirement { get; set; }
    public int RequiredCount { get; set; }
    public string Description { get; set; }
}