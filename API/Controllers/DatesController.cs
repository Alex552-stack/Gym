using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class DatesController(
    QrCodeService qrCodeService,
    GymDbContext gymDbContext,
    UserManager<AppUser> userManager,
    AccountService accountService,
    GymVisitsService gymVisitsService) : BaseApiController
{
    private readonly QrCodeService _qrCodeService = qrCodeService;
    private readonly GymDbContext _context = gymDbContext;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly GymVisitsService _gymVisitsService = gymVisitsService;
    private static readonly object _lock = new object();
    private readonly AccountService _accountService = accountService;

    [HttpPost("ScanQrCode")]
    public async Task<IActionResult> ScanQrCode([FromQuery] string qrCodeData)
    {
        if (User.Identity?.Name == null)
            return Unauthorized();
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return BadRequest("Trebuie sa fi conectat pentru a putea face aceasta actiune");

        if (!_qrCodeService.ValidateQrCode(qrCodeData, out DateTime visitDate))
            return BadRequest("Cod invalid");
        lock (_lock)
        {
            var lastVisit = _context.Set<GymVisit>().Where(v =>
                v.UserId == user.Id && v.Year == DateTime.UtcNow.Year && v.Mounth == DateTime.UtcNow.Month &&
                v.Day == DateTime.UtcNow.Day).FirstOrDefault();

            var isTracking = _context.ChangeTracker.Entries<GymVisit>()
                .Any(v => v.Entity.UserId == user.Id);

            if (isTracking)
                return BadRequest("Nu ai voie sa scanezi codul QR de mai multe ori pe zi");

            if (lastVisit != default(GymVisit))
                return BadRequest("Nu ai voie sa scanezi codul QR de mai multe ori pe zi");


            GymVisit visit = new GymVisit(user.Id, visitDate);
            _context.Add(visit);
            _context.SaveChanges();
            return Ok();

        }
    }

    [HttpGet("GetGymVisits")]
    public async Task<IActionResult> GetGymVisits()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
            return BadRequest("Trebuie sa fi conectat pentru a putea dace aceasta actiune");

        var dates = await _context.Set<GymVisit>().Where(v => v.UserId == user.Id).ToListAsync();
        return Ok(
            new
            {
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
    public async Task<IActionResult> GetCalendarDates()
    {
        var user = await _accountService.GetCurrentUser(User.Identity.Name);

        if (user == null) return Unauthorized("Trebuie sa fi conectat pentru a putea efectua aceasta operatie");

        var dates = await _context.Set<GymVisit>().Where(v => v.UserId == user.Id).ToListAsync();

        return Ok(dates);
    }

    [HttpPost("Test")]
    public async Task<IActionResult> PostVisit(int day, int mounth, int year)
    {
        var user = await _userManager.FindByNameAsync("alex");

        var visit = new GymVisit
        {
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

    [HttpGet("interval")]
    public async Task<IActionResult> GetInInterval(DateTime timeStart, DateTime timeStop)
    {
        var user = await _accountService.GetCurrentUser(User.Identity.Name);

        if (user == null) return Unauthorized("Trebuie sa fi conectat pentru a putea efectua aceasta operatie");

        var dates = await _gymVisitsService.GetVisitsForUser(user.Id, timeStart, timeStop);

        return Ok(dates);
    }
}