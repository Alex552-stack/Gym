using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class GymVisitsService(GymDbContext context)
{
    private readonly GymDbContext _context = context;

    public async Task<List<GymVisit>> GetAllGymVIsits(int userId)
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
        var CurrentTime = DateTime.Now;
        var visitCount = await _context.Set<GymVisit>().Where(v => v.UserId == userId
                                                            && v.Year == CurrentTime.Year
                                                            && v.Mounth == CurrentTime.Month
                                                            && v.Day == CurrentTime.Day).CountAsync();
        return visitCount != 0;
    }

    public async Task<bool> AdminAddGymVisit(int userId, DateTime time)
    {
        await _context.Set<GymVisit>().AddAsync(
            new GymVisit(userId, time));
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Tiers?> GetGymVisitsForUser(int idUser)
    {
        IEnumerable<Tiers> tiers = await _context.Set<Tiers>().OrderByDescending(t => t.RequiredCount).ToListAsync();
        IEnumerable<GymVisit> visits = await GetAllGymVIsits(idUser);
        
        foreach (var tier in tiers)
        {
            DateTime cutoffDate = DateTime.Now - tier.TimeToCompleteRequirement;
            
            int visitCount = visits.Count(v => new DateTime(v.Year, v.Mounth, v.Day) >= cutoffDate);
            
            if (visitCount >= tier.RequiredCount)
            {
                return tier;
            }
        }

        // Return null if no tier is matched
        return null;
    }

    public async Task<Tiers> NextTier(Tiers currentTier)
    {
        var nextTier = await _context.Set<Tiers>().Where(t => t.Id == currentTier.Id + 1).FirstOrDefaultAsync();
        return nextTier;
    }
    
    public async Task<List<GymVisit>> GetVisitsForUser(int userId, DateTime startDate, DateTime endDate)
    {
        startDate = startDate.ToLocalTime();
        endDate = endDate.ToLocalTime();
        return await _context.Set<GymVisit>().Where(v => v.UserId == userId
                                                          && new DateTime(v.Year, v.Mounth, v.Day) >= startDate
                                                          && new DateTime(v.Year, v.Mounth, v.Day) <= endDate)
                                                          .ToListAsync();
    }
}