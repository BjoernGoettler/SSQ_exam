using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class GetAllUsersAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }

    private IGraduationService _graduationService;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.Users.AddRangeAsync(new Models.User { Id = 1, Name = "User1" },
            new Models.User { Id = 2, Name = "User2" });
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestGetAllUsersAsync()
    {
        var users = await _graduationService.GetAllUsersAsync();

        Assert.Multiple(() =>
        {
            Assert.That(users, Is.Not.Null);
            Assert.That(users.Count, Is.EqualTo(2));
        });
    }
}