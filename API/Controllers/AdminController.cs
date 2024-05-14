using API.Data.Context.GymDbContext;
using API.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[AllowAnonymous]

public class AdminController : BaseApiController
{
    private readonly GymDbContext _context;
    public AdminController(GymDbContext context) : base()
    {
        _context = context;
    }
    
    [HttpPost("keyValuePair")]
    public async Task<IActionResult> KeyValuePair(AuxData data)
    {
        bool alreadyExists = await _context.Set<AuxData>().AsNoTracking().AnyAsync(d => d.Key == data.Key);

        if (alreadyExists)
        {
            return Conflict("A resource with the same key already exists");
        }

        _context.Set<AuxData>().Add(data);
        int change = await _context.SaveChangesAsync();


        return CreatedAtAction("KeyValuePair", new { key = data.Key }, data);

    }

    [HttpGet("keyValuePair")]
    public async Task<IActionResult> KeyValuePair(string id)
    {
        return Ok(await _context.Set<AuxData>().FindAsync(id));
    }

    [HttpGet("keyValuePair/list")]
    public async Task<ActionResult<List<string>>> KeyValuePairList()
    {
        return Ok(await _context.Set<AuxData>().Select(d => d.Key).ToListAsync());
    }
}