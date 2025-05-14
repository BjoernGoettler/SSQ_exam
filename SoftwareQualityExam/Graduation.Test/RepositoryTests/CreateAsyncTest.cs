using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Repositories;
using Graduation.Test.Fixtures;

namespace Graduation.Test.RepositoryTests;

[TestFixture]
public class CreateAsyncTest: TestBase
{
    private IGraduationRepository _graduationRepository ;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
    }
    
    [Test]
    public async Task TestCreateAsync()
    {
        GraduationDetail graduationDetail = new GraduationDetail
        {
            Name = "TestGraduation",
            GraduationDate = new DateOnly(2025, 5, 1),
            CreatedAt = DateTime.Now
        };
        
        var actualGraduationDetail = await _graduationRepository.CreateAsync(graduationDetail);
        
        Assert.Multiple(() =>
        {
            Assert.That(actualGraduationDetail, Is.Not.Null);
            Assert.That(actualGraduationDetail.Id, Is.GreaterThan(0));
            Assert.That(actualGraduationDetail.Name, Is.EqualTo(graduationDetail.Name));
            Assert.That(actualGraduationDetail.GraduationDate, Is.EqualTo(graduationDetail.GraduationDate));
            Assert.That(actualGraduationDetail.CreatedAt, Is.EqualTo(graduationDetail.CreatedAt));
        });
    }
}