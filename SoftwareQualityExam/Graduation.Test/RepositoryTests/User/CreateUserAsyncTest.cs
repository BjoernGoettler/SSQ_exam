using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;

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
    
    [Test]
    public async Task TestCreateUserAsync()
    {
        var expectedUser = new Models.User
        {
            Id = 1,
            Name = "TestUser"
        };
        
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