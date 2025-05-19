using Graduation.Core;

namespace Graduation.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Ranks Rank { get; set; }
    public int Kalis { get; set; }
}