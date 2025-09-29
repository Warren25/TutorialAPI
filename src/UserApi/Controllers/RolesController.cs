namespace UserApi.Controllers;

[ApiController]
[Route("tutorialapi/[controller]")]
public class RolesController : ControllerBase
{
    private readonly UserDbContext _dbContext;

    public RolesController(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _dbContext.Roles.ToListAsync();
        return Ok(roles);
    }
}