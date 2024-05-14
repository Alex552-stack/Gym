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
        var data = await _context.Set<AuxData>().FindAsync(id);
        if(data is null) return NotFound(id);
        return Ok(data);
    }

    [HttpGet("keyValuePair/list")]
    public async Task<ActionResult<List<string>>> KeyValuePairList()
    {
        return Ok(await _context.Set<AuxData>().Select(d => d.Key).ToListAsync());
    }

    [HttpPut("keyValuePair")]
    public async Task<ActionResult> Put(AuxData data)
    {
        _context.Set<AuxData>().Update(data);
        var result = await _context.SaveChangesAsync();

        if(result == 0)
        return UnprocessableEntity(data);

        return Ok(data);
    }

    [HttpDelete("keyValuePair")]
    public async Task<ActionResult> Delete([FromQuery] string key)
    {
        var toBeDeleted = await _context.Set<AuxData>().FindAsync(key);
        if (toBeDeleted is null)
            return NotFound("Nu a fost gasita nicio informatie cu cheia " + key);
        
        _context.Set<AuxData>().Remove(toBeDeleted);
        var result = await _context.SaveChangesAsync();

        if(result != 0)
        {
            return Ok(toBeDeleted);
        }

        return UnprocessableEntity(toBeDeleted);
    }
}