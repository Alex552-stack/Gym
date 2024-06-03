using API.Data.Entities;

namespace API.Dtos;

public class TierDto
{
    public TierDto(Tiers tier)
    {
        Id = tier.Id;
        Name = tier.Name;
        TimeToCompleteRequirement = tier.TimeToCompleteRequirement;
        RequiredCount = tier.RequiredCount;
        Description = tier.Description;
    }
    
    public int? Id { get; set; }
    public string? Name { get; set; }
    public TimeSpan TimeToCompleteRequirement { get; set; }
    public int? RequiredCount { get; set; }
    public string? Description { get; set; }
}