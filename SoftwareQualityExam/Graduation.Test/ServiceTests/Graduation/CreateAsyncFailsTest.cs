using Graduation.Exceptions;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.Graduation;

[TestFixture]
public class CreateAsyncFailsTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }

    private IGraduationService _graduationService;
    private GraduationDetail _graduationDetail;

    private async Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.AddAsync(TestGraduationDetails.ValidGraduationDetails1);
    }

    [Test]
    public void TestCreateAsyncFails()
    {
        var graduationDetailIn = DtoGraduationDetails.DuplicateDtoGraduationDetailIn;
        Assert.ThrowsAsync<DuplicateEntryException>(() => _graduationService.CreateAsync(graduationDetailIn));
    }
}