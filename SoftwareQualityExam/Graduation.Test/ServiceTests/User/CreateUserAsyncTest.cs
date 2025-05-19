using Graduation.DataTransferObjects;
using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class CreateUserAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
    }

    private IGraduationService _graduationService;
    private readonly UserIn _userIn = DtoUsers.ValidDtoUserIn1;

    [Test]
    public async Task TestCreateUserAsync()
    {
        var actualUser = await _graduationService.CreateUserAsync(_userIn);

        Assert.Multiple(() =>
        {
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(actualUser.Id, Is.GreaterThan(0));
            Assert.That(actualUser.Name, Is.EqualTo(_userIn.Name));
        });
    }
}