using Graduation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Test.Fixtures;

public abstract class TestBase
{
    protected DatabaseContext DbContext => TestDatabaseFixture.DbContext;
    
    [SetUp]
    public virtual void SetUp()
    {
        // Common setup for all tests
        // Consider transaction management for test isolation
    }
    
    [TearDown]
    public virtual void TearDown()
    {
        // Common cleanup for all tests
    }
}