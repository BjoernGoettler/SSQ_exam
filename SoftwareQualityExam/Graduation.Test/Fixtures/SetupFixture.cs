using System;
using Graduation.Infrastructure;
using Microsoft.Data.Sqlite;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Graduation.Test;

    [SetUpFixture]
    public class TestDatabaseFixture
    {
        public static DatabaseContext DbContext { get; private set; }
        private static ServiceProvider _serviceProvider;
        private static SqliteConnection _connection;
        
        [OneTimeSetUp]
        public void InitializeDatabase()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            var services = new ServiceCollection();
            
            services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite(_connection), 
                ServiceLifetime.Singleton);
            
            _serviceProvider = services.BuildServiceProvider();
            DbContext = _serviceProvider.GetRequiredService<DatabaseContext>();
            
            // Run migrations
            DbContext.Database.Migrate();
            
            // Seed initial test data if needed
            SeedTestData();
        }
        
        private void SeedTestData()
        {
            
        }
        
        [OneTimeTearDown]
        public void CleanupDatabase()
        {
            _connection?.Close();
            DbContext?.Dispose();
            _serviceProvider?.Dispose();
            
        }
    }
