using Microsoft.EntityFrameworkCore;

namespace Graduation.Models;

[Index(nameof(GraduationDate), IsUnique = true)]
public class GraduationDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly GraduationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}