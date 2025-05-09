using Graduation.Infrastructure;
using Microsoft.EntityFrameworkCore;

static string FindSolutionRoot()
{
    var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
    
    // Navigate up until we find a .sln file or reach the drive root
    while (directory != null && !directory.GetFiles("*.sln").Any())
    {
        directory = directory.Parent;
    }
    
    return directory?.FullName ?? Directory.GetCurrentDirectory();
}

var solutionRoot = FindSolutionRoot();
Console.WriteLine($"Solution root: {solutionRoot}");

var databaseEnvFilePath = Path.Combine(solutionRoot, "database.env");
var envFilePath = Path.Combine(solutionRoot, ".env");

if (File.Exists(databaseEnvFilePath))
{
    try
    {
        DotNetEnv.Env.Load(databaseEnvFilePath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading database.env: {ex.Message}");
    }
}

if (File.Exists(envFilePath))
{
    try
    {
        DotNetEnv.Env.Load(envFilePath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading .env: {ex.Message}");
    }
}

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("defaultConnection");
connectionString = connectionString
    .Replace("${DATABASE_HOST}", Environment.GetEnvironmentVariable("DATABASE_HOST"))
    .Replace("${MYSQL_DATABASE}", Environment.GetEnvironmentVariable("MYSQL_DATABASE"))
    .Replace("${MYSQL_USER}", Environment.GetEnvironmentVariable("MYSQL_USER"))
    .Replace("${MYSQL_PASSWORD}", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29)); // Adjust version as needed

// Register DbContext with MySQL
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionString, serverVersion, 
        mysqlOptions => 
        {
            // Enable retrying failed database operations
            mysqlOptions.EnableRetryOnFailure(
                maxRetryCount: 1,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();