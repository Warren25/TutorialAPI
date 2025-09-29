using Microsoft.EntityFrameworkCore;
using UserCore.Data;

namespace UserApi.Services;

public interface IUserStatsService
{
    Task<int> GetTotalUsersAsync();
    Task<int> GetTotalRolesAsync();
    string GetInstanceId();
}

public class UserStatsService : IUserStatsService
{
    private readonly UserDbContext _dbContext;
    private readonly string _instanceId;

    public UserStatsService(UserDbContext dbContext)
    {
        _dbContext = dbContext;
        _instanceId = Guid.NewGuid().ToString("N")[..8]; // Short ID for this instance
        Console.WriteLine($"UserStatsService created with ID: {_instanceId}");
    }

    public async Task<int> GetTotalUsersAsync()
    {
        return await _dbContext.Users.CountAsync();
    }

    public async Task<int> GetTotalRolesAsync()
    {
        return await _dbContext.Roles.CountAsync();
    }

    public string GetInstanceId() => _instanceId;
}