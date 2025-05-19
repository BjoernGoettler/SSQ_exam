using Graduation.DataTransferObjects;
using Graduation.Exceptions;
using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Migrations;
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

        try
        {
            var actualGraduationDetail = await _graduationRepository.CreateAsync(graduationDetail);
            return new GraduationDetailOut
            {
                Id = actualGraduationDetail.Id,
                Name = actualGraduationDetail.Name,
                GraduationDate = actualGraduationDetail.GraduationDate,
                CreatedAt = actualGraduationDetail.CreatedAt
            };

        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
        {
            if (e.InnerException is Microsoft.Data.Sqlite.SqliteException)
            {
                if (e.InnerException.Message.StartsWith("SQLite Error 19: 'UNIQUE constraint failed"))
                {
                    var errorMessage =
                        $"Another graduation is already registered on {graduationDetailIn.GraduationDate}.";
                    throw new DuplicateEntryException(errorMessage);
                }
            }
            
            throw;
        }
    }
    
    public async Task<UserOut> CreateUserAsync(UserIn userIn)
    {
        var user = new User
        {
            Name = userIn.Name
        };
        var createdUser = await _graduationRepository.CreateUserAsync(user);
        return new UserOut
        {
            Id = createdUser.Id,
            Name = createdUser.Name
        };
    }
    
    public async Task<List<UserOut>> GetAllUsersAsync()
    {
        var users = await _graduationRepository.GetAllUsersAsync();
        return users.Select(x => new UserOut
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}