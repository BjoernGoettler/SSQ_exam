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
}