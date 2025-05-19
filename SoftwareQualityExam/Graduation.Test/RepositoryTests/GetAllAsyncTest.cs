using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
namespace Graduation.Test.RepositoryTests;

[TestFixture]
public class GetAllAsyncTest: TestBase
{
    private GraduationDetail _graduationDetail;
    private int expectedId = 1;
    
    
    private IGraduationRepository _graduationRepository ;
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        
        // Optionally: Seed specific test data for this test class
        await SeedTestData();
    }
    
    private Task SeedTestData()
    {
        // Add test graduation data if needed
        if (!DbContext.GraduationDetails.Any())
        {
            _graduationDetail = new GraduationDetail
            {
                Id = expectedId,
                Name = "TestGraduation",
                GraduationDate = new DateOnly(2025, 5, 1),
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
        var allDetails = await _graduationRepository.GetAllAsync();
        Assert.Multiple(() =>
        {
            Assert.That(allDetails, Is.Not.Empty);
            Assert.That(allDetails.Count, Is.EqualTo(1));
            Assert.That(allDetails[0].Id, Is.EqualTo(expectedId));

        });
    }
}