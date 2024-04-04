using System.Text;
using API.Data.Context;
using API.Data.Context.GymDbContext;
using API.Data.Entities;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GymDbContext>(
    option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
/*builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GymDbContext>()
    .AddDefaultTokenProviders();*/
builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<GymDbContext>()
    .AddDefaultTokenProviders(); ;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes("PlaceholderKeyssssssssssssssssssssssssssssssssaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaasdasdasdasdasdasdsdsdsdsdsdsdsddddddddddddsssssss"))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Use authentication
app.UseAuthorization();

app.MapControllers();


var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
ApplicationDbInitializer.SeedRoles(scope.ServiceProvider.GetRequiredService<RoleManager<Role>>());
app.Run();
