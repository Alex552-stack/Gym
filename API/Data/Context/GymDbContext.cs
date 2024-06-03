using API.Data.Entities;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Context.GymDbContext;

public class GymDbContext(DbContextOptions options) : IdentityDbContext<AppUser, Role, int>(options)
{
    public DbSet<TestData> Data { get; set; }
    public DbSet<AuxData> AuxDates { get; set; }
    public DbSet<GymVisit> GymVisits { get; set; } 
    public DbSet<Tiers> Tiers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        builder.Entity<Tiers>().HasData(
            new Tiers()
            {
                Id = 1,
                Description = "The basic tear, easy for newcomers to achieve",
                Name = "Iron Tier",
                RequiredCount = 10,
                TimeToCompleteRequirement = TimeSpan.FromDays(30) // 1 month
            },
            new Tiers()
            {
                Id = 2,
                Description = "The first real milestone. You are starting to get stronger",
                Name = "Bronze Tier",
                RequiredCount = 30,
                TimeToCompleteRequirement = TimeSpan.FromDays(60) // 2 months
            },
            new Tiers()
            {
                Id = 3,
                Description = "Now you are starting to impress people with your physique. Keep going",
                Name = "Iron Tier",
                RequiredCount = 40,
                TimeToCompleteRequirement = TimeSpan.FromDays(90) // 3 months
            },
            new Tiers()
            {
                Id = 4,
                Description = "The gym is your second home. Or maybe even the firts...",
                Name = "Gold Tier",
                RequiredCount = 60,
                TimeToCompleteRequirement = TimeSpan.FromDays(120) // 4 months
            }
        );

    }
}