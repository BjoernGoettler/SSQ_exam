using Graduation.Interfaces;
using Graduation.Services;
using Graduation.Test.Fixtures;
using Graduation.Test.TestData;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.ServiceTests.Graduation;

[TestFixture]
public class CreateAsyncTest : TestBase
{
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationService = new GraduationService(DbContext);
        await SeedTestData();
    }

    private IGraduationService _graduationService;

    private async Task SeedTestData()
    {
        await DbContext.GraduationDetails.ExecuteDeleteAsync();
        await DbContext.SaveChangesAsync();
    }

    [Test]
    public async Task TestCreateAsync()
    {
        var expectedGraduationDetailIn = DtoGraduationDetails.ValidDtoGraduationDetailIn;
        var actualGraduationDetail = await _graduationService.CreateAsync(expectedGraduationDetailIn);

        Assert.Multiple(() =>
        {
            Assert.That(actualGraduationDetail, Is.Not.Null);
            Assert.That(actualGraduationDetail.Id, Is.GreaterThan(0));
            Assert.That(actualGraduationDetail.Name, Is.EqualTo(expectedGraduationDetailIn.Name));
            Assert.That(actualGraduationDetail.GraduationDate, Is.EqualTo(expectedGraduationDetailIn.GraduationDate));
        });
    }
}