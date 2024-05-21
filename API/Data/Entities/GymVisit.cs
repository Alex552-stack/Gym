using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data.Entities;

public class GymVisit
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public AppUser User { get; set; }

    public int Year { get; set; }
    public int Mounth { get; set; }
    public int Day { get; set; } 

    public GymVisit(int userId, DateTime date)
    {
        UserId = userId;
        Year = date.Year;
        Mounth = date.Month;
        Day = date.Day;
    }
    public GymVisit(){}
}