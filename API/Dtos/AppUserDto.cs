using API.Data.Entities;
using API.Entities;

namespace API.Dtos;

public class AppUserDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public bool EmailConfirmed { get; set; }
    public int NumberofTotalVisits { get; set; }
    public Tiers UnlockedTier { get; set; }
    public Tiers NextTier { get; set; }
    public List<string> Roles { get; set; }
}