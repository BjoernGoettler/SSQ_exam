using Graduation.DataTransferObjects;

namespace Graduation.Test.TestData;

public static class DtoGraduationDetails
{
    
    public static readonly GraduationDetailIn ValidDtoGraduationDetailIn = new GraduationDetailIn
    {
        Name = "Valid DTO Graduation Detail",
        GraduationDate = Dates.FixedDate
    };

    public static readonly GraduationDetailIn DuplicateDtoGraduationDetailIn = new GraduationDetailIn
    {
        Name = "Duplicate DTO Graduation Detail",
        GraduationDate = Dates.FixedDate
    };

    public static readonly GraduationDetailIn[] ValidGraduationDetailsIn = new[]
    {
        new GraduationDetailIn
        {
            Name = "First Valid DTO Graduation Detail in list",
            GraduationDate = new DateOnly(2025, 5, 13)
        },
        new GraduationDetailIn
        {
            Name = "Second Valid DTO Graduation Detail in list",
            GraduationDate = new DateOnly(2025, 5, 14)
        }
    };

}