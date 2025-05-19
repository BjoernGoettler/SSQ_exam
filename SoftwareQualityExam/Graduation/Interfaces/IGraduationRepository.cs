using Graduation.Models;

namespace Graduation.Interfaces;

public interface IGraduationRepository
{
    public Task<List<GraduationDetail>> GetAllAsync();
    public Task<GraduationDetail> GetByIdAsync(int id);
    public Task<GraduationDetail> CreateAsync(GraduationDetail graduationDetail);
    public Task<bool> UpdateAsync(GraduationDetail graduationDetail);
    public Task<bool> DeleteAsync(int id);
    public Task<User> CreateUserAsync(User user);
    public Task<User> GetUserByIdAsync(int id);
    public Task<bool> DeleteUserAsync(int id);
    public Task<User> UpdateUserAsync(User user);
    public Task<List<User>> GetAllUsersAsync();
}