using System.Collections.Immutable;
using Graduation.Interfaces;
using Graduation.Repositories;
using Graduation.Test.Fixtures;

namespace Graduation.Test.RepositoryTests.User;

[TestFixture]
public class GetAllUsersAsyncTest: TestBase
{
    private IGraduationRepository _graduationRepository;
    private Models.User _expectedUser = new Models.User
    {
        Id = 11111,
        Name = "TestUser"
    };
    
    [SetUp]
    public override async Task SetUp()
    {
        await base.SetUp();
        _graduationRepository = new GraduationRepository(DbContext);
        DbContext.Users.Add(_expectedUser);
        await DbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task TestGetAllUsersAsync()
    {
        var actualUserList = await _graduationRepository.GetAllUsersAsync();
        Assert.Multiple(() =>
            {
                Assert.That(actualUserList, Is.Not.Null);
                Assert.That(actualUserList.Count, Is.AtLeast(1));;
            }
            );

    }
    
}