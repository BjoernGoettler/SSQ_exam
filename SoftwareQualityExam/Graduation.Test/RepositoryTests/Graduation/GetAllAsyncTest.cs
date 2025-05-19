using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.Graduation;

[TestFixture]
public class GetAllAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);

        // Optionally: Seed specific test data for this test class
        await SeedTestData();
    }

    private GraduationDetail _graduationDetail;
    private readonly int expectedId = 1;


    private IGraduationRepository _graduationRepository;

    private async Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.GraduationDetails.AddRangeAsync(TestGraduationDetails.ValidGraduationDetails1,
            TestGraduationDetails.ValidGraduationDetails2);
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestGetAllAsync()
    {
        var allDetails = await _graduationRepository.GetAllAsync();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Is.Not.Empty);
            Assert.That(allDetails.Count, Is.EqualTo(2));
            Assert.That(allDetails[0].Id, Is.EqualTo(expectedId));
        });
    }
    
    [TearDown]
    public override async Task TearDown()
    {
        // Clean up resources if needed
        await base.TearDown();
    }
}