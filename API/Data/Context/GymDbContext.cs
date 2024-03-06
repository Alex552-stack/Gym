using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Context.GymDbContext;

public class GymDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TestData> Data { get; set; }
}