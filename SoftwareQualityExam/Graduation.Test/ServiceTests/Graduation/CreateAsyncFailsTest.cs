using Graduation.DataTransferObjects;
using Graduation.Exceptions;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.Graduation;

[TestFixture]

public class CreateAsyncFailsTest: TestBase
{
    private IGraduationService _graduationService ;
    private GraduationDetail _graduationDetail;
    
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }
    
    private async Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.AddAsync(TestData.TestGraduationDetails.ValidGraduationDetails1);
    }
    
    [Test]
    public void TestCreateAsyncFails()
    {
        GraduationDetailIn graduationDetailIn = TestData.DtoGraduationDetails.DuplicateDtoGraduationDetailIn;
        Assert.ThrowsAsync<DuplicateEntryException>(() => _graduationService.CreateAsync(graduationDetailIn));
    }
    
}