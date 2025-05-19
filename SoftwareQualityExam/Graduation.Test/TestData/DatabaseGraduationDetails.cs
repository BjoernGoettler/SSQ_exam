using Graduation.Models;
namespace Graduation.Test.TestData;


public static class TestGraduationDetails
{
    public static readonly GraduationDetail ValidGraduationDetails1 = new GraduationDetail
    {
        Name = "Valid Test Graduation 1",
        GraduationDate = Dates.FixedDate,
        CreatedAt = DateTime.Now
    };
    
    public static readonly GraduationDetail ValidGraduationDetails2 = new GraduationDetail
    {
        Name = "Valid Test Graduation 2",
        GraduationDate = new DateOnly(2025, 5, 2),
        CreatedAt = DateTime.Now
    };

    public static readonly GraduationDetail DuplicateGraduationDetail = new GraduationDetail
    {
        Name = "Duplicate Test Graduation",
        GraduationDate = Dates.FixedDate,
    };

    public static readonly GraduationDetail[] ValidGraduationDetails = new[]
    {
        new GraduationDetail
        {
            Name = "First Valid Test Graduation in list",
            GraduationDate = new DateOnly(2025, 5, 13),
            CreatedAt = DateTime.Now
        },
        new GraduationDetail
        {
            Name = "Second Valid Test Graduation in list",
            GraduationDate = new DateOnly(2025, 5, 14),
        }
    };


}