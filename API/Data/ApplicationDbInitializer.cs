using API.Data.Context.GymDbContext;
using API.Data.Entities;
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

    public static void SeedAuxData(GymDbContext context)
    {
        if (!context.Set<AuxData>().Any())
        {
            AuxData[] auxData = {
                new() {
                    Key = "LocationCount",
                    Data = "1"
                },
                new() {
                    Key = "EmployeesCount",
                    Data = "1"
                },
                new() {
                    Key = "CustomerCount",
                    Data = "1"
                },
                new() {
                    Key = "SubscriptionPlan1",
                    Data = "150 lei/Luna",
                    Details = "Planul Basic"
                },
                new() {
                    Key = "SubscriptionPlan2",
                    Data = "100 lei/Luna",
                    Details = "Planul Mid"
                },
                new() {
                    Key = "SubscriptionPlan3",
                    Data = "50 lei/Luna",
                    Details = "Planul Gold"
                }
            };
            context.AddRange(auxData);
            context.SaveChanges();
        }
    }
}
