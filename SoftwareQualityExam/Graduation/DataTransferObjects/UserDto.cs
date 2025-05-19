using Graduation.Core;

namespace Graduation.DataTransferObjects;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Ranks Rank { get; set; }
    public bool Kali { get; set; }
}