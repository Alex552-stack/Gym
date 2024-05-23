using API.Data.Entities;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Context.GymDbContext;

public class GymDbContext(DbContextOptions options) : IdentityDbContext<AppUser, Role, int>(options)
{
    public DbSet<TestData> Data { get; set; }
    public DbSet<AuxData> AuxDatas { get; set; }
    public DbSet<GymVisit> GymVisits { get; set; } 

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
    }
}