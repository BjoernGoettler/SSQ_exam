using Graduation.Infrastructure;
using Graduation.Interfaces;

namespace Graduation.Repositories;

public class GraduationRepository: IGraduationRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public GraduationRepository(DatabaseContext databaseContext)
        => _databaseContext = databaseContext;
    
}