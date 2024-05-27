using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class DatesController(QrCodeService qrCodeService, GymDbContext gymDbContext, UserManager<AppUser> userManager, AccountService accountService) : BaseApiController
{
    private readonly QrCodeService _qrCodeService = qrCodeService;
    private readonly GymDbContext _context = gymDbContext;
    private readonly UserManager<AppUser> _userManager = userManager;

    private readonly AccountService _accountService = accountService;

    [HttpPost("ScanQrCode")]
    public async Task<IActionResult> ScanQrCode(string qrCodeData)
    {
        if(User.Identity?.Name == null)
            return Unauthorized();
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return BadRequest("Trebuie sa fi conectat pentru a putea face aceasta actiune");
        if (_qrCodeService.ValidateQrCode(qrCodeData, out DateTime visitDate))
        {
            GymVisit visit = new GymVisit(user.Id, visitDate);
            await _context.AddAsync(visit);
            return Ok();
        }
        return BadRequest("QrCode invalid");
    }

    [HttpGet("GetGymVisits")]
    public async Task<IActionResult> GetGymVisits()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if(user == null)
            return BadRequest("Trebuie sa fi conectat pentru a putea dace aceasta actiune");
        
        var dates = await _context.Set<GymVisit>().Where(v => v.UserId == user.Id).ToListAsync();
        return Ok(
            new{
                dates,
                dates.Count
            }
        );
    }

    [HttpGet("GetQrCode")]
    public IActionResult GetQrCode()
    {
        return Ok(_qrCodeService.GenerateQrCode(DateTime.Now));
    }

    [HttpGet("Dates")]
    public async  Task<IActionResult> GetCalendarDates()
    {
        var user = await _accountService.GetCurrentUser(User.Identity.Name);

        if(user == null) return Unauthorized("Trebuie sa fi conectat pentru a putea efectua aceasta operatie");

        var dates = await _context.Set<GymVisit>().Where(v => v.UserId == user.Id).ToListAsync();

        return Ok(dates);

    }

    [HttpPost("Test")]
    public async Task<IActionResult> PostVisit(int day, int mounth, int year)
    {
        var user = await _userManager.FindByNameAsync("alex");

        var visit = new GymVisit{
                User = user,
                UserId = user.Id,
                Year = year,
                Mounth = mounth,
                Day = day
            };

        _context.Set<GymVisit>().Add(
            visit
        );

        _context.SaveChanges();

        return Ok(visit);
    }

}