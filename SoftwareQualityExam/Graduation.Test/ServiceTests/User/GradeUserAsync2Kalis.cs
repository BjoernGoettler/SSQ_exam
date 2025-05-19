using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class GradeUserAsync2Kalis : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        _graduationService = new GraduationService(DbContext);
        await DbContext.Database.EnsureCreatedAsync();
        await SeedTestData();
    }

    private IGraduationService _graduationService;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        
        await DbContext.Users.AddAsync(DatabaseUsers.UserWith2Kalis);
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestGradeUserAsync2Kalis()
    {
        var user = DtoUsers.UserWith2Kalis;
        var actualUserOut = await _graduationService.GradeUserAsync(user);

        Assert.Multiple(() =>
        {
            Assert.That(actualUserOut, Is.Not.Null);
            Assert.That(actualUserOut.Id, Is.EqualTo(user.Id));
            Assert.That(actualUserOut.Rank, Is.EqualTo(DatabaseUsers.UserWith2Kalis.Rank));
            Assert.That(actualUserOut.Kalis, Is.EqualTo(0));
        });
    }
    
}