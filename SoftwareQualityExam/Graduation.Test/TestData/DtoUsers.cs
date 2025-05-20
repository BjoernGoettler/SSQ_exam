using Graduation.DataTransferObjects;

namespace Graduation.Test.TestData;

public static class DtoUsers
{
    public static readonly UserInNew ValidDtoUserIn1 = new()
    {
        Name = "Valid DTO User 1"
    };

    public static readonly UserInNew ValidDtoUserIn2 = new()
    {
        Name = "Valid DTO User 2"
    };

    public static readonly UserDto DemotedUserDto = new()
    {
        Id = SharedValues.demotedUserId,
        Name = "Demoted User",
        Rank = SharedValues.demotedUserRankAfterPromotion,
        Kali = false
    };

    public static readonly UserDto UserWith2Kalis = new()
    {
        Id = SharedValues.userWith2Kalis,
        Name = "User with 2 kalis",
        Rank = SharedValues.userWith2KalisRank,
        Kali = true
    };
}