using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserApi.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Controllers support
builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register our custom services with different lifetimes
builder.Services.AddSingleton<ICounterService, CounterService>();
builder.Services.AddTransient<IGuidService, GuidService>();
builder.Services.AddScoped<IUserStatsService, UserStatsService>(); // NEW: Scoped service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Commented out to allow both HTTP and HTTPS

// Add health check endpoint
app.MapGet("/health", () => "Healthy");

// Map controllers instead of individual endpoints
app.MapControllers();

app.Run();
