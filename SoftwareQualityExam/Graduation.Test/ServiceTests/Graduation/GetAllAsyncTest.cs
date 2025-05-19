using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.Graduation;

[TestFixture]
public class GetAllAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);

        // Optionally: Seed specific test data for this test class
        await SeedTestData();
    }

    private GraduationDetail _graduationDetail;


    private IGraduationService _graduationService;

    private async Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();
        var remaining = await DbContext.GraduationDetails.CountAsync();
        Assert.That(remaining, Is.EqualTo(0), "Database should be empty after cleanup");


        await DbContext.GraduationDetails.AddRangeAsync(TestGraduationDetails.ValidGraduationDetails);
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
    
    [TearDown]
    public override async Task TearDown()
    {
        // Clean up resources if needed
        await base.TearDown();
    }

}