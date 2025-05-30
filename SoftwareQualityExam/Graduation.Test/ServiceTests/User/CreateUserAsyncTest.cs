using Graduation.DataTransferObjects;
using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class CreateUserAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }

    private IGraduationService _graduationService;
    private readonly UserInNew _userInNew = DtoUsers.ValidDtoUserIn1;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();
    }


    [Test]
    public async Task TestCreateUserAsync()
    {
        var actualUser = await _graduationService.CreateUserAsync(_userInNew);

        Assert.Multiple(() =>
        {
            Assert.That(actualUser, Is.Not.Null);
            Assert.That(actualUser.Id, Is.GreaterThan(0));
            Assert.That(actualUser.Name, Is.EqualTo(_userInNew.Name));
        });
    }
    [TearDown]
    public override async Task TearDown()
    {
        
        await base.TearDown();

    }
}