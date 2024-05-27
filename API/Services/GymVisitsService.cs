using API.Data.Context.GymDbContext;
using API.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GymVisitsService(GymDbContext context)
{
    private readonly GymDbContext _context = context;

    public async Task<List<GymVisit>> GetGimVisitsForUser(int userId)
    {
        List<GymVisit> gymVisits = await _context.Set<GymVisit>().Where(g => g.UserId == userId).ToListAsync();
        return gymVisits;
    }

    public async Task<bool> AddGymVisitFor(int userId)
    {
        if (await UserVisitedToday(userId))
            return false;
        
        GymVisit visit = new(userId, DateTime.Now);
        await _context.Set<GymVisit>().AddAsync(visit);

        int columnsAffected = await _context.SaveChangesAsync();
        return columnsAffected != 0;
    }

    private async Task<bool> UserVisitedToday(int userId)
    {
        var currenttime = DateTime.Now;
        var visitCount = await _context.Set<GymVisit>().Where(v => v.UserId == userId
                                                            && v.Year == currenttime.Year
                                                            && v.Mounth == currenttime.Month
                                                            && v.Day == currenttime.Day).CountAsync();
        return visitCount != 0;
    }

    public async Task<bool> AdminAddGymVisit(int userId, DateTime time)
    {
        await _context.Set<GymVisit>().AddAsync(
            new GymVisit(userId, time));
        _context.SaveChangesAsync();
        return true;
    }
}