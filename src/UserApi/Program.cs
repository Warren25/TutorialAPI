using Microsoft.EntityFrameworkCore;
using UserCore.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.Run();
