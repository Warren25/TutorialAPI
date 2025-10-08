using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserApi.Services; 

var builder = WebApplication.CreateBuilder(args);

// Smart HTTPS configuration - only enable if certificate exists
var certPath = builder.Configuration["ASPNETCORE_Kestrel__Certificates__Default__Path"];
var httpUrls = "http://+:8080";
var httpsUrls = "http://+:8080;https://+:8081";

if (!string.IsNullOrEmpty(certPath) && File.Exists(certPath))
{
    Console.WriteLine($"✅ Certificate found at {certPath}. HTTPS enabled.");
    builder.WebHost.UseUrls(httpsUrls);
}
else
{
    Console.WriteLine("⚠️  No certificate found. Running HTTP-only mode.");
    builder.WebHost.UseUrls(httpUrls);
}

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
