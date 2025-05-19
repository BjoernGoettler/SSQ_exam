using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.User;

[TestFixture]
public class GetAllUsersAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        await SeedTestData();
    }

    private IGraduationRepository _graduationRepository;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.Users.AddRangeAsync(DatabaseUsers.ValidDatabaseUser3, DatabaseUsers.ValidDatabaseUser4);
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestGetAllUsersAsync()
    {
        var actualUserList = await _graduationRepository.GetAllUsersAsync();
        Assert.Multiple(() =>
            {
                Assert.That(actualUserList, Is.Not.Null);
                Assert.That(actualUserList.Count, Is.AtLeast(2));
                ;
            }
        );
    }
    
    [TearDown]
    public override async Task TearDown()
    {
        // Clean up resources if needed
        await base.TearDown();
    }
}