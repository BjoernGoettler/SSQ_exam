using Graduation.Core;

namespace Graduation.DataTransferObjects;

public class UserOut
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Ranks Rank { get; set; }
    public int Kalis { get; set; }
}