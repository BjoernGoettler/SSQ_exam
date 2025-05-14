using Graduation.DataTransferObjects;
using Graduation.Exceptions;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests;

public class CreateAsyncFailsTest: TestBase
{
    private IGraduationService _graduationService ;
    private GraduationDetail _graduationDetail;
    private readonly DateOnly _graduationDate = new DateOnly(2025, 5, 1);
    
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
        
        _graduationDetail = new GraduationDetail
        {
            Name = "TestGraduation",
            GraduationDate = _graduationDate,
            CreatedAt = DateTime.Now
        };
        DbContext.GraduationDetails.Add(_graduationDetail);
        
        DbContext.SaveChanges();
    }
    
    [Test]
    public async Task TestCreateAsyncFails()
    {
        GraduationDetailIn graduationDetailIn = new GraduationDetailIn
        {
            Name = "Failing graduation",
            GraduationDate = _graduationDate
        };
        //var _ = await _graduationService.CreateAsync(graduationDetailIn);
        //var something = DbContext.GraduationDetails.ToList();
        Assert.ThrowsAsync<DuplicateEntryException>(() => _graduationService.CreateAsync(graduationDetailIn));
    }
    
}