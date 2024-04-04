namespace API.Dtos;

public class AppUserDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public bool EmailConfirmed { get; set; }
}