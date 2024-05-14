using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Entities;

public class AuxData
{
    [Key, MaxLength(55)]
    public string Key { get; set; }
    public string Data { get; set; }  
    public string? Details {get; set; }
}