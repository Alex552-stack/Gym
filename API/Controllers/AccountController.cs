using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Dtos;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;
        private readonly GymDbContext _context;
        //private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly GymVisitsService _visitsService;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(GymVisitsService visitsService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, UserManager<AppUser> userManager, TokenService tokenService, GymDbContext context/*, EmailService emailService*/)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;
            _visitsService = visitsService;

        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            Tiers tier = await _visitsService.GetGymVisitsForUser(user.Id);
            Tiers nextTier = await _visitsService.NextTier(tier);
            int visitCount = (await _visitsService.GetAllGymVIsits(user.Id)).Count;
            //await _visitsService.AddGymVisitFor(user.Id);
            
            return new AppUserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                EmailConfirmed = user.EmailConfirmed,
                NumberofTotalVisits = (int)visitCount,
                UnlockedTier = (Tiers)tier,
                NextTier = nextTier,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if(existingUser != null)
            {
                return BadRequest($"Deja exista un user adresa de email {user.Email}");
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);


            await _userManager.AddToRoleAsync(user, "Member");

            var message = $"Your verification link is {confirmationLink}";

            //_visitsService.AddGymVisitFor(user.Id);

            return Ok("User created");
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            Tiers? tier = null;
            Tiers? nextTier = null;
            int? visitCount = 0;
            
            if (user is not null)
            {
                tier = await _visitsService.GetGymVisitsForUser(user.Id);
                visitCount = (await _visitsService.GetAllGymVIsits(user.Id)).Count;
                nextTier = await _visitsService.NextTier(tier);
            }

            return new AppUserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                EmailConfirmed = user.EmailConfirmed,
                NumberofTotalVisits = (int)visitCount,
                UnlockedTier = (Tiers)tier,
                NextTier = nextTier,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };
        }

        /*[Authorize]
        [HttpGet("savedAddress")]
        public async Task<ActionResult<UserAddress>> GetSavedAddress()
        {
            return await _userManager.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(user => user.Address)
                .FirstOrDefaultAsync();
        }*/

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _context.SaveChangesAsync();
                string redirectUrl;
                if (_webHostEnvironment.IsDevelopment())
                {
                    var request = HttpContext.Request;
                    redirectUrl = $"{request.Scheme}://{request.Host}/checkemail/1";
                }
                else
                {
                    redirectUrl = "https://restore43221.fly.dev/checkemail";
                }
                return Redirect(redirectUrl);
            }
            else
            {
                return BadRequest("Email confirmation failed.");
            }
        }

    }
}