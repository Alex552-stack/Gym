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

string connString;
// z
    connString = builder.Configuration.GetConnectionString("DefaultConnection");
//else
{
//     // Use connection string provided at runtime by FlyIO.
//     var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

//     // Parse connection URL to connection string for Npgsql
//     connUrl = connUrl.Replace("postgres://", string.Empty);
//     var pgUserPass = connUrl.Split("@")[0];
//     var pgHostPortDb = connUrl.Split("@")[1];
//     var pgHostPort = pgHostPortDb.Split("/")[0];
//     var pgDb = pgHostPortDb.Split("/")[1];
//     var pgUser = pgUserPass.Split(":")[0];
//     var pgPass = pgUserPass.Split(":")[1];
//     var pgHost = pgHostPort.Split(":")[0];
//     var pgPort = pgHostPort.Split(":")[1];
//     var updatedHost = pgHost.Replace("flycast", "internal");

// connString = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
}

builder.Services.AddDbContext<GymDbContext>(
    option =>
    {
        option.UseNpgsql(connString);
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
builder.Services.AddScoped<QrCodeService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<GymVisitsService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseCors(opt => opt
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials() // Allows credentials such as cookies, authorization headers, or TLS client certificates
    .WithOrigins("http://localhost:3000")
    .WithExposedHeaders("pagination") // Specify the exact origin
);

app.UseRouting();

//app.useHttp();
app.UseAuthentication(); // Use authentication

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

context.Database.Migrate();

ApplicationDbInitializer.SeedRoles(scope.ServiceProvider.GetRequiredService<RoleManager<Role>>());
ApplicationDbInitializer.SeedAuxData(scope.ServiceProvider.GetRequiredService<GymDbContext>());
ApplicationDbInitializer.SeedUser(scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>());
app.Run();
