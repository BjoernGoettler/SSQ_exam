namespace Graduation.Models;

public class GraduationDetail
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly GraduationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}