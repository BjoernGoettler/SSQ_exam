using Graduation.Core;
using Graduation.Models;

namespace Graduation.Test.TestData;

public static class DatabaseUsers
{
    public static readonly User ValidDatabaseUser1 = new()
    {
        Id = 1,
        Name = "Valid Test User 1",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User ValidDatabaseUser2 = new()
    {
        Id = 2,
        Name = "Valid Test User 2",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User ValidDatabaseUser3 = new()
    {
        Id = 3,
        Name = "Valid Test User 3",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User ValidDatabaseUser4 = new()
    {
        Id = 4,
        Name = "Valid Test User 4",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User ValidDatabaseUser5 = new()
    {
        Name = "Valid Test User 5",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User ValidDatabaseUser6 = new()
    {
        Name = "Valid Test User 6",
        Rank = Ranks.Kyu10,
        Kalis = 0
    };

    public static readonly User DemotedUser = new()
    {
        Id = SharedValues.demotedUserId,
        Name = "Demoted User",
        Rank = SharedValues.demotedUserRank,
        Kalis = 0
    };

    public static readonly User UserWith2Kalis = new()
    {
        Id = SharedValues.userWith2Kalis,
        Name = "User with 2 Kalis",
        Rank = SharedValues.userWith2KalisRank,
        Kalis = 2
    };
}