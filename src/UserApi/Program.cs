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

app.UseHttpsRedirection();

// Add test endpoints to see the difference
app.MapGet("/test/singleton", (ICounterService counter) =>
{
    counter.Increment();
    return new { Count = counter.GetCount(), Message = "Singleton - same instance across requests" };
})
.WithName("TestSingleton")
.WithOpenApi();

app.MapGet("/test/transient", (IGuidService guidService) =>
{
    return new { Guid = guidService.GetGuid(), Message = "Transient - new instance each request" };
})
.WithName("TestTransient")
.WithOpenApi();

app.MapGet("/test/scoped", async (IUserStatsService statsService) =>
{
    var totalUsers = await statsService.GetTotalUsersAsync();
    var totalRoles = await statsService.GetTotalRolesAsync();
    
    return new { 
        TotalUsers = totalUsers,
        TotalRoles = totalRoles,
        InstanceId = statsService.GetInstanceId(),
        Message = "Scoped - same instance within request, new per request"
    };
})
.WithName("TestScoped")
.WithOpenApi();

// User management endpoints
app.MapGet("/users", async (UserDbContext db) =>
{
    return await db.Users.ToListAsync();
})
.WithName("GetUsers")
.WithOpenApi();

app.MapGet("/roles", async (UserDbContext db) =>
{
    return await db.Roles.ToListAsync();
})
.WithName("GetRoles")
.WithOpenApi();

// Map controllers instead of individual endpoints
app.MapControllers();

app.Run();
