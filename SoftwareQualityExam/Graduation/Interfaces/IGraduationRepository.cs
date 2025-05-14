using Graduation.Models;

namespace Graduation.Interfaces;

public interface IGraduationRepository
{
    public Task<List<GraduationDetail>> GetAllAsync();
    public Task<GraduationDetail> GetByIdAsync(int id);
    public Task<GraduationDetail> CreateAsync(GraduationDetail graduationDetail);
    public Task<bool> UpdateAsync(GraduationDetail graduationDetail);
    public Task<bool> DeleteAsync(int id);
}