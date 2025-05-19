using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.RepositoryTests.Graduation;

[TestFixture]
public class CreateAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        await SeedTestData();
    }

    private IGraduationRepository _graduationRepository;

    private async Task SeedTestData()
    {
        await DbContext.Users.ExecuteDeleteAsync();
    }

    [Test]
    public async Task TestCreateAsync()
    {
        var graduationDetail = TestGraduationDetails.ValidGraduationDetails1;

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