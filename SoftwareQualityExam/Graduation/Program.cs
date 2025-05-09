using Graduation.Infrastructure;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load("database.env");
DotNetEnv.Env.Load(".env");


var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
                maxRetryCount: 5,
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