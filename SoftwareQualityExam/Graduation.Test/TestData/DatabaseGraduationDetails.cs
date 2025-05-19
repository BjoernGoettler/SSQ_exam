using Graduation.Models;

namespace Graduation.Test.TestData;

public static class TestGraduationDetails
{
    public static readonly GraduationDetail ValidGraduationDetails1 = new()
    {
        Name = "Valid Test Graduation 1",
        GraduationDate = Dates.May1St,
        CreatedAt = DateTime.Now
    };

    public static readonly GraduationDetail ValidGraduationDetails2 = new()
    {
        Name = "Valid Test Graduation 2",
        GraduationDate = new DateOnly(2025, 5, 2),
        CreatedAt = DateTime.Now
    };

    public static readonly GraduationDetail ValidGraduationDetails3 = new()
    {
        Name = "Valid Test Graduation 3",
        GraduationDate = new DateOnly(2025, 6, 2),
        CreatedAt = DateTime.Now
    };

    public static readonly GraduationDetail ValidGraduationDetails4 = new()
    {
        Name = "Valid Test Graduation 4",
        GraduationDate = new DateOnly(2025, 6, 3),
        CreatedAt = DateTime.Now
    };

    public static readonly GraduationDetail DuplicateGraduationDetail = new()
    {
        Name = "Duplicate Test Graduation",
        GraduationDate = Dates.August1St
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
            GraduationDate = new DateOnly(2025, 5, 14)
        }
    };
}