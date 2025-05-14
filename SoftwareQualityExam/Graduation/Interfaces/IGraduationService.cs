using Graduation.DataTransferObjects;
using Graduation.Models;

namespace Graduation.Interfaces;

public interface IGraduationService
{
    public Task<List<GraduationDetailOut>> GetAllAsync();
    
    public Task<GraduationDetailOut> CreateAsync(GraduationDetailIn graduationDetail);
}