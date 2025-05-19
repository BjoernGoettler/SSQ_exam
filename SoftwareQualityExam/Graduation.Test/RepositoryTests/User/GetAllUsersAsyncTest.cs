using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.User;

[TestFixture]
public class GetAllUsersAsyncTest: TestBase
{
    private IGraduationRepository _graduationRepository;
    
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        await SeedTestData();
    }
    
    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.Users.AddRangeAsync(new[]
        {
            TestData.DatabaseUsers.ValidDatabaseUser3,
            TestData.DatabaseUsers.ValidDatabaseUser4,
        });
        await DbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task TestGetAllUsersAsync()
    {
        var actualUserList = await _graduationRepository.GetAllUsersAsync();
        Assert.Multiple(() =>
            {
                Assert.That(actualUserList, Is.Not.Null);
                Assert.That(actualUserList.Count, Is.AtLeast(2));;
            }
            );

    }
    
}