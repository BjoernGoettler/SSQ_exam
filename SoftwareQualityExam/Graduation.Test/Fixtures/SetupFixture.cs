using Graduation.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Graduation.Test.Fixtures;

    [SetUpFixture]
    public class TestDatabaseFixture
    {
        private static readonly object Lock = new();
        private static bool _initialized;

        public static DatabaseContext DbContext { get; private set; }
        private static ServiceProvider _serviceProvider;
        private static SqliteConnection _connection;
        
        [OneTimeSetUp]
        public async Task InitializeDatabase()
        {
            if (_initialized) return;
            lock (Lock)
            {
                if (_initialized) return;
                _connection = new SqliteConnection("Data Source=:memory:");
                _connection.Open();
                var services = new ServiceCollection();
            
                services.AddDbContext<DatabaseContext>(options =>
                        options.UseSqlite(_connection)
                            .EnableSensitiveDataLogging(), 
                    ServiceLifetime.Singleton);
            
                _serviceProvider = services.BuildServiceProvider();
                DbContext = _serviceProvider.GetRequiredService<DatabaseContext>();
            
                // Run migrations
                DbContext.Database.EnsureCreated();
            
                // Seed initial test data if needed
                
                _initialized = true;
            }
            await SeedTestData();
            
        }
        
        private async Task SeedTestData()
        {
            await DbContext.SaveChangesAsync();
        }
        
        [OneTimeTearDown]
        public async Task CleanupDatabase()
        {
            await _connection.CloseAsync();
            await DbContext.DisposeAsync();
            await _serviceProvider.DisposeAsync();
            _initialized = false;
        }
    }
