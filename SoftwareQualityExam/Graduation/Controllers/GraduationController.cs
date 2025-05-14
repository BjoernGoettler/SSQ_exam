using Graduation.DataTransferObjects;
using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Services;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers;

public class GraduationController: ControllerBase
{
    private readonly IGraduationService _graduationService;
    private readonly DatabaseContext _databaseContext;

    public GraduationController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _graduationService = new GraduationService(_databaseContext);
    }
    
    [HttpGet]
    [Route("api/graduation")]
    public async Task<ActionResult<List<GraduationDetailOut>>> GetAllAsync()
        => await _graduationService.GetAllAsync();

}