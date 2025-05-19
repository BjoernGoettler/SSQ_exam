using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests;

[TestFixture]
public class GetAllAsyncTest: TestBase
{
    private GraduationDetail _graduationDetail;
    
    
    private IGraduationService _graduationService ;
    [SetUp]
    public override async Task SetUp()
    {
        base.SetUp();
        _graduationService = new GraduationService(DbContext);
        
        // Optionally: Seed specific test data for this test class
        await SeedTestData();
    }
    
    private Task SeedTestData()
    {
        DbContext.GraduationDetails.ExecuteDelete();
        
        if (!DbContext.GraduationDetails.Any())
        {
            _graduationDetail = new GraduationDetail
            {
                Name = "TestGraduation",
                GraduationDate = new DateOnly(2025, 5, 2),
                CreatedAt = DateTime.Now
            };
            DbContext.GraduationDetails.Add(_graduationDetail);
            
            DbContext.SaveChanges();
        }
        return Task.CompletedTask;
    }

    [Test]
    public async Task TestGetAllAsync()
    {
        var allDetails = await _graduationService.GetAllAsync();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Is.Not.Empty);
            Assert.That(allDetails.Count, Is.AtLeast(1));

        });
    }
}