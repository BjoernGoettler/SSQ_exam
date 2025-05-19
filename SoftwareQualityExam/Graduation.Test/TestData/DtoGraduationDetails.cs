using Graduation.DataTransferObjects;

namespace Graduation.Test.TestData;

public static class DtoGraduationDetails
{
    public static readonly GraduationDetailIn ValidDtoGraduationDetailIn1 = new()
    {
        Name = "Valid DTO Graduation Detail",
        GraduationDate = new DateOnly(2026, 1, 1)
    };

    public static readonly GraduationDetailIn DuplicateDtoGraduationDetailIn = new()
    {
        Name = "Duplicate DTO Graduation Detail",
        GraduationDate = Dates.August1St
    };

    public static readonly GraduationDetailIn[] ValidGraduationDetailsIn = new[]
    {
        new GraduationDetailIn
        {
            Name = "First Valid DTO Graduation Detail in list",
            GraduationDate = new DateOnly(2025, 5, 15)
        },
        new GraduationDetailIn
        {
            Name = "Second Valid DTO Graduation Detail in list",
            GraduationDate = new DateOnly(2025, 5, 16)
        }
    };
}