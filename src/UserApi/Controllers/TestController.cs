using Microsoft.AspNetCore.Mvc;
using UserApi.Services;

namespace UserApi.Controllers;

[ApiController]
[Route("tutorialapi/[controller]")]
public class TestController : ControllerBase
{
    private readonly ICounterService _counterService;
    private readonly IGuidService _guidService;
    private readonly IUserStatsService _userStatsService;

    public TestController(
        ICounterService counterService, 
        IGuidService guidService,
        IUserStatsService userStatsService)
    {
        _counterService = counterService;
        _guidService = guidService;
        _userStatsService = userStatsService;
    }

    [HttpGet("singleton")]
    public IActionResult TestSingleton()
    {
        _counterService.Increment();
        return Ok(new { 
            Count = _counterService.GetCount(), 
            Message = "Singleton - same instance across requests" 
        });
    }

    [HttpGet("transient")]
    public IActionResult TestTransient()
    {
        return Ok(new { 
            Guid = _guidService.GetGuid(), 
            Message = "Transient - new instance each request" 
        });
    }

    [HttpGet("scoped")]
    public async Task<IActionResult> TestScoped()
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