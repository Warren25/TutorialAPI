using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserCore.Data;
using UserApi.Services;

namespace UserApi.Controllers;

[ApiController]
[Route("tutorialapi/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserDbContext _dbContext;
    private readonly IUserStatsService _userStatsService;

    public UsersController(UserDbContext dbContext, IUserStatsService userStatsService)
    {
        _dbContext = dbContext;
        _userStatsService = userStatsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _dbContext.Users.ToListAsync();
        return Ok(users);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetUserStats()
    {
        var totalUsers = await _userStatsService.GetTotalUsersAsync();
        var totalRoles = await _userStatsService.GetTotalRolesAsync();
        
        return Ok(new { 
            TotalUsers = totalUsers,
            TotalRoles = totalRoles,
            InstanceId = _userStatsService.GetInstanceId(),
            Message = "Scoped - same instance within request, new per request"
        });
    }
}