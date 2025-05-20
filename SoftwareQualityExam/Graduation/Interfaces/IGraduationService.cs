using Graduation.DataTransferObjects;

namespace Graduation.Interfaces;

public interface IGraduationService
{
    public Task<List<GraduationDetailOut>> GetAllAsync();

    public Task<GraduationDetailOut> CreateAsync(GraduationDetailIn graduationDetail);

    public Task<UserOut> CreateUserAsync(UserInNew user);

    public Task<List<UserOut>> GetAllUsersAsync();

    public Task<UserOut> GradeUserAsync(UserDto userDto);
}