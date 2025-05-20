using Graduation.Core;
using Graduation.DataTransferObjects;
using Graduation.Exceptions;
using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Models;
using Graduation.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Services;

public class GraduationService : IGraduationService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IGraduationRepository _graduationRepository;

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
        var graduationDetail = new GraduationDetail
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
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqliteException)
                if (e.InnerException.Message.StartsWith("SQLite Error 19: 'UNIQUE constraint failed"))
                {
                    var errorMessage =
                        $"Another graduation is already registered on {graduationDetailIn.GraduationDate}.";
                    throw new DuplicateEntryException(errorMessage);
                }

            throw;
        }
    }

    public async Task<UserOut> CreateUserAsync(UserInNew userInNew)
    {
        var user = new User
        {
            Name = userInNew.Name,
            Rank = Ranks.Kyu10,
            Kalis = 0
        };
        var createdUser = await _graduationRepository.CreateUserAsync(user);
        return new UserOut
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Rank = createdUser.Rank,
            Kalis = createdUser.Kalis
        };
    }

    public async Task<List<UserOut>> GetAllUsersAsync()
    {
        var users = await _graduationRepository.GetAllUsersAsync();
        return users.Select(x => new UserOut
        {
            Id = x.Id,
            Name = x.Name,
            Rank = x.Rank,
            Kalis = x.Kalis
        }).ToList();
    }

    public async Task<UserOut> GradeUserAsync(UserDto userDto)
    {
        User updatedUser;

        var user = await _graduationRepository.GetUserByIdAsync(userDto.Id);

        // Make sure the grated karate ka is not getting a lower rank
        if (user.Rank > userDto.Rank)
        {
            var errorMessage = "Downgrading a karate kas rank is disallowed";
            throw new RankDemotionException(errorMessage);
        }

        if (!userDto.Kali)
        {
            user.Rank = userDto.Rank;
            user.Kalis = 0;
            updatedUser = await _graduationRepository.UpdateUserAsync(user);
            return new UserOut
            {
                Id = user.Id,
                Name = user.Name,
                Rank = user.Rank,
                Kalis = user.Kalis
            };
        }

        if (userDto.Kali && user.Kalis == 2)
        {
            user.Kalis = 0;
            updatedUser = await _graduationRepository.UpdateUserAsync(user);
            return new UserOut
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Rank = updatedUser.Rank,
                Kalis = updatedUser.Kalis
            };
        }

        user.Kalis = userDto.Kali ? user.Kalis + 1 : user.Kalis;
        user.Rank = userDto.Rank;

        updatedUser = await _graduationRepository.UpdateUserAsync(user);

        return new UserOut
        {
            Id = updatedUser.Id,
            Name = updatedUser.Name,
            Rank = updatedUser.Rank,
            Kalis = updatedUser.Kalis
        };
    }
}