using Graduation.DataTransferObjects;
using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Repositories;

namespace Graduation.Services;

public class GraduationService: IGraduationService
{
    private readonly IGraduationRepository _graduationRepository;
    private readonly DatabaseContext _databaseContext;

    public GraduationService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _graduationRepository = new GraduationRepository(_databaseContext);
    }

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

    public async Task<GraduationDetailOut> CreateAsync(GraduationDetailIn graduationDetailIn)
    {
        GraduationDetail graduationDetail = new GraduationDetail
        {
            Name = graduationDetailIn.Name,
            GraduationDate = graduationDetailIn.GraduationDate,
            CreatedAt = DateTime.Now
        };
        
        var actualGraduationDetail = await _graduationRepository.CreateAsync(graduationDetail);

        return new GraduationDetailOut
        {
            Id = actualGraduationDetail.Id,
            Name = actualGraduationDetail.Name,
            GraduationDate = actualGraduationDetail.GraduationDate,
            CreatedAt = actualGraduationDetail.CreatedAt
        };
    }
}