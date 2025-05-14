using Graduation.DataTransferObjects;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests;

[TestFixture]
public class CreateAsyncTest: TestBase
{
    private IGraduationService _graduationService ;
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _graduationService = new GraduationService(DbContext);
        SeedTestData();
    }
    
    private void SeedTestData()
    {
        DbContext.GraduationDetails.ExecuteDelete();
        DbContext.SaveChanges();
    }
    
    [Test]
    public async Task TestCreateAsync()
    {
        GraduationDetailIn graduationDetailIn = new GraduationDetailIn
        {
            Name = "TestGraduation",
            GraduationDate = new DateOnly(2025, 5, 3)
        };
        var actualGraduationDetail = await _graduationService.CreateAsync(graduationDetailIn);
        
        Assert.Multiple(() =>
        {
            Assert.That(actualGraduationDetail, Is.Not.Null);
            Assert.That(actualGraduationDetail.Id, Is.GreaterThan(0));
            Assert.That(actualGraduationDetail.Name, Is.EqualTo(graduationDetailIn.Name));
            Assert.That(actualGraduationDetail.GraduationDate, Is.EqualTo(graduationDetailIn.GraduationDate));
        });
        
        
    }
    
}