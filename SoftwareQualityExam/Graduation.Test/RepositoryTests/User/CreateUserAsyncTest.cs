using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.User;

[TestFixture]
public class CreateUserAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        await SeedTestData();
    }

    private IGraduationRepository _graduationRepository;

    public async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestCreateUserAsync()
    {
        var expectedUser = DatabaseUsers.ValidDatabaseUser1;

        var actualUser = await _graduationRepository.CreateUserAsync(expectedUser);

        Assert.Multiple(() =>
            {
                Assert.That(actualUser, Is.Not.Null);
                Assert.That(actualUser.Id, Is.EqualTo(expectedUser.Id));
                Assert.That(actualUser.Name, Is.EqualTo(expectedUser.Name));
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