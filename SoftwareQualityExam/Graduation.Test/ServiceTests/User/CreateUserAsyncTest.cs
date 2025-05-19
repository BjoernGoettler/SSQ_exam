using Graduation.DataTransferObjects;
using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class CreateUserAsyncTest: TestBase
{
    private IGraduationService _graduationService;

    private readonly UserIn userIn = new UserIn
    {
        Name = "Test User"
    };
    
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
    }

    [Test]
    public async Task TestCreateUserAsync()
    {
        var actualUser = await _graduationService.CreateUserAsync(userIn);
        
        Assert.Multiple(() =>
        {
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(actualUser.Id, Is.GreaterThan(0));
            Assert.That(actualUser.Name, Is.EqualTo(userIn.Name));
        });
        
        
    }
    
}