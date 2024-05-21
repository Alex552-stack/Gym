using Microsoft.AspNetCore.Identity;

namespace API.Data.Entities;

public class AppUser : IdentityUser<int>
{
     public ICollection<GymVisit> GymVisits { get; set; }
}