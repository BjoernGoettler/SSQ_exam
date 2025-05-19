using Graduation.Infrastructure;
using Graduation.Interfaces;
using Graduation.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Repositories;

public class GraduationRepository : IGraduationRepository
{
    private readonly DatabaseContext _databaseContext;

    public GraduationRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<GraduationDetail>> GetAllAsync()
    {
        return await _databaseContext.GraduationDetails.ToListAsync();
    }

    public async Task<GraduationDetail> GetByIdAsync(int id)
    {
        var graduationDetail = await _databaseContext.GraduationDetails.FirstAsync(x => x.Id == id);
        if (graduationDetail == null) throw new KeyNotFoundException($"GraduationDetail with id {id} not found.");
        return graduationDetail;
    }

    public async Task<GraduationDetail> CreateAsync(GraduationDetail graduationDetail)
    {
        await _databaseContext.GraduationDetails.AddAsync(graduationDetail);
        await _databaseContext.SaveChangesAsync();
        return graduationDetail;
    }

    public async Task<bool> UpdateAsync(GraduationDetail graduationDetail)
    {
        _databaseContext.GraduationDetails.Update(graduationDetail);
        var result = await _databaseContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var graduationDetail = await GetByIdAsync(id);
        _databaseContext.GraduationDetails.Remove(graduationDetail);
        var result = await _databaseContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _databaseContext.Users.AddAsync(user);
        await _databaseContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _databaseContext.Users.FirstAsync(x => x.Id == id);
        if (user == null) throw new KeyNotFoundException($"User with id {id} not found.");

        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _databaseContext.Users.Update(user);
        var _ = await _databaseContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await GetUserByIdAsync(id);
        _databaseContext.Users.Remove(user);
        var result = await _databaseContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _databaseContext.Users.ToListAsync();
    }
}