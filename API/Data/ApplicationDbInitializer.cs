using API.Entities;
using Microsoft.AspNetCore.Identity;

public static class ApplicationDbInitializer
{
    public static void SeedRoles(RoleManager<Role> roleManager)
    {
        if (!roleManager.RoleExistsAsync("MEMBER").Result)
        {
            Role role = new()
            {
                Name = "MEMBER"
            };
            roleManager.CreateAsync(role).Wait();
        }
    }
}
