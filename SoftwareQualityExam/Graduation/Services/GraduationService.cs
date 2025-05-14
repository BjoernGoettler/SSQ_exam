using Graduation.DataTransferObjects;
using Graduation.Infrastructure;
using Graduation.Interfaces;

namespace Graduation.Services;

public class GraduationService: IGraduationService
{
    private readonly IGraduationRepository _graduationRepository;
    private readonly DatabaseContext _databaseContext;
    
    public GraduationService(DatabaseContext databaseContext)
        => _databaseContext = databaseContext;

    public async Task<List<GraduationDetailOut>> GetAllAsync()
    {
        var graduationDetails = await _graduationRepository.GetAllAsync();
        return graduationDetails.Select(x => new GraduationDetailOut
        {
            Id = x.Id,
            Name = x.Name,
            GraduationDate = x.GraduationDate,
            CreatedAt = x.CreatedAt
        }).ToList();
    }
}