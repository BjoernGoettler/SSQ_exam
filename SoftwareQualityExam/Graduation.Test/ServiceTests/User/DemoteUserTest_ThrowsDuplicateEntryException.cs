using Graduation.Exceptions;
using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.User;

[TestFixture]
public class DemoteUserTest_ThrowsDuplicateEntryException : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }

    private IGraduationService _graduationService;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.ExecuteDeleteAsync();

        await DbContext.Users.AddAsync(DatabaseUsers.DemotedUser);
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestDemoteUserFails()
    {
        var user = DtoUsers.DemotedUserDto;

        Assert.ThrowsAsync<RankDemotionException>(() => _graduationService.GradeUserAsync(user));
    }
    
    [TearDown]
    public override async Task TearDown()
    {
        
        await base.TearDown();

    }
}