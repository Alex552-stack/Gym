using API.Data.Context.GymDbContext;
using API.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class TiersController(GymDbContext context) : BaseApiController
{
    private readonly GymDbContext _context = context;


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _context.Set<Tiers>().FindAsync(id);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Set<Tiers>().OrderBy(o => o.Id).ToListAsync());

    [HttpPut]
    public async Task<IActionResult> Edit(Tiers tier)
    {
        var entity = await _context.Set<Tiers>().FirstOrDefaultAsync(t => t.Id == tier.Id);
        if (entity is default(Tiers))
            return BadRequest($"Entitatea cu id-ul {tier.Id} nu exista");

        entity.Description = tier.Description;
        entity.Name = tier.Name;
        entity.RequiredCount = tier.RequiredCount;
        entity.TimeToCompleteRequirement = tier.TimeToCompleteRequirement;
        var rez = await _context.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Tiers tier)
    {
        var data = await _context.Set<Tiers>().AddAsync(tier);
        await _context.SaveChangesAsync();
        return Ok(tier);
    }
}