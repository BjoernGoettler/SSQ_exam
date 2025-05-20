using Graduation.DataTransferObjects;
using Graduation.Exceptions;
using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Services;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers;

public class GraduationController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;
    private readonly IGraduationService _graduationService;

    public GraduationController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _graduationService = new GraduationService(_databaseContext);
    }

    [HttpGet]
    [Route("api/graduation")]
    public async Task<ActionResult<List<GraduationDetailOut>>> GetAllAsync()
    {
        return await _graduationService.GetAllAsync();
    }

    [HttpPost]
    [Route("api/graduation")]
    public async Task<ActionResult<GraduationDetailOut>> CreateAsync([FromBody] GraduationDetailIn graduationDetailIn)
    {
        try
        {
            return await _graduationService.CreateAsync(graduationDetailIn);
        }
        catch (DuplicateEntryException)
        {
            var errorMessage =
                $"Another graduation is already registered on this date {graduationDetailIn.GraduationDate}.";
            return Conflict(errorMessage);
        }
    }

    [HttpPost]
    [Route("api/user")]
    public async Task<ActionResult<UserOut>> CreateUserAsync([FromBody] UserInNew userInNew)
        => await _graduationService.CreateUserAsync(userInNew);
    
    [HttpGet]
    [Route("api/user")]
    public async Task<ActionResult<List<UserOut>>> GetAllUsersAsync()
        => await _graduationService.GetAllUsersAsync();

    [HttpPut]
    [Route("api/user/grade")]
    public async Task<ActionResult<UserOut>> GradeUserAsync([FromBody] UserDto userDto)
    {
        try
        {
            var user = await _graduationService.GradeUserAsync(userDto);
            return user;

        }
        catch (Graduation.Exceptions.RankDemotionException e)
        {
            return UnprocessableEntity(e.Message);
        }
    } 
    
}