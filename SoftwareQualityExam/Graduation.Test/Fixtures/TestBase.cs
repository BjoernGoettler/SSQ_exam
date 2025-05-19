using Graduation.Infrastructure;

namespace Graduation.Test.Fixtures;

public abstract class TestBase
{
    private static readonly SemaphoreSlim _initLock = new(1, 1);
    private static readonly TestDatabaseFixture TestDatabaseFixture = new();
    protected DatabaseContext DbContext => TestDatabaseFixture.DbContext;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await TestDatabaseFixture.InitializeDatabase();
    }

    [SetUp]
    public virtual async Task SetUp()
    {
        await _initLock.WaitAsync();
        try
        {
            // Common setup for all tests
            // Consider transaction management for test isolation
            await DbContext.Database.EnsureCreatedAsync();
        }
        finally
        {
            _initLock.Release();
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        // Common cleanup for all tests
        await OnTearDown();
    }

    protected virtual Task OnTearDown()
    {
        return Task.CompletedTask;
    }
}