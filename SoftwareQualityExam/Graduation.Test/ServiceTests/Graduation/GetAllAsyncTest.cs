using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.Graduation;

[TestFixture]
public class GetAllAsyncTest: TestBase
{
    private GraduationDetail _graduationDetail;
    
    
    private IGraduationService _graduationService ;
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        
        // Optionally: Seed specific test data for this test class
        await SeedTestData();
    }
    
    private async  Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.AddRangeAsync(TestData.TestGraduationDetails.ValidGraduationDetails);
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestGetAllAsync()
    {
        var allDetails = await _graduationService.GetAllAsync();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Is.Not.Empty);
            Assert.That(allDetails.Count, Is.AtLeast(2));

        });
    }
}