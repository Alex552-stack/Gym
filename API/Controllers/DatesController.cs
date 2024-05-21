using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class DatesController(QrCodeService qrCodeService, GymDbContext gymDbContext, UserManager<AppUser> userManager) : BaseApiController
{
    private readonly QrCodeService _qrCodeService = qrCodeService;
    private readonly GymDbContext _context = gymDbContext;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpPost("ScanQrCode")]
    public async Task<IActionResult> ScanQrCode(string qrCodeData)
    {
        if(User.Identity.Name == null)
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
        
        int visits = await _context.Set<GymVisit>().CountAsync(v => v.UserId == user.Id);
        return Ok(visits);
    }

    [HttpGet("GetQrCode")]
    public IActionResult GetQrCode()
    {
        return Ok(_qrCodeService.GenerateQrCode(DateTime.Now));
    }

}