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
    public override void SetUp()
    {
        base.SetUp();
        _graduationService = new GraduationService(DbContext);
        
        // Optionally: Seed specific test data for this test class
        SeedTestData();
    }
    
    private void SeedTestData()
    {
        DbContext.GraduationDetails.ExecuteDelete();
        // Add test graduation data if needed
        if (!DbContext.GraduationDetails.Any())
        {
            _graduationDetail = new GraduationDetail
            {
                Name = "TestGraduation",
                GraduationDate = new DateOnly(2025, 5, 1),
                CreatedAt = DateTime.Now
            };
            DbContext.GraduationDetails.Add(_graduationDetail);
            
            DbContext.SaveChanges();
        }
    }

    [Test]
    public async Task TestGetAllAsync()
    {
        var allDetails = await _graduationService.GetAllAsync();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Is.Not.Empty);
            Assert.That(allDetails.Count, Is.EqualTo(1));

        });
    }
}