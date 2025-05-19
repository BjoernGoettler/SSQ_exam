using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.User;

[TestFixture]
public class CreateUserAsyncTest: TestBase
{
    private IGraduationRepository _graduationRepository ;

    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
    }

    public async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
    }
    
    [Test]
    public async Task TestCreateUserAsync()
    {
        var expectedUser = TestData.DatabaseUsers.ValidDatabaseUser1;
        
        var actualUser = await _graduationRepository.CreateUserAsync(expectedUser);
        
        Assert.Multiple(() =>
            {
                Assert.That(actualUser, Is.Not.Null);
                Assert.That(actualUser.Id, Is.EqualTo(expectedUser.Id));
                Assert.That(actualUser.Name, Is.EqualTo(expectedUser.Name));
            }
            );

    }
}