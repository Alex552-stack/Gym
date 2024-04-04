using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly TokenService _tokenService;
        private readonly GymDbContext _context;
        //private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly UserManager<AppUser> _userManager;
        public AccountController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, UserManager<AppUser> userManager, TokenService tokenService, GymDbContext context/*, EmailService emailService*/)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _context = context;
            _tokenService = tokenService;
            _userManager = userManager;

        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();
            return new AppUserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                EmailConfirmed = user.EmailConfirmed
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

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

            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<AppUserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new AppUserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                EmailConfirmed = user.EmailConfirmed
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