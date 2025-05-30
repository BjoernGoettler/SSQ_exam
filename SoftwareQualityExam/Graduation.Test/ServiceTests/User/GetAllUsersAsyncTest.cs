using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
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
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.Users.AddRangeAsync(DatabaseUsers.ValidDatabaseUser5, DatabaseUsers.ValidDatabaseUser6);
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
    [TearDown]
    public override async Task TearDown()
    {
        // Clean up resources if needed
        await base.TearDown();
    }
   
}