using Data.Models;

namespace Blazor.Data;

public interface IGradeService
{
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(StatisticsDTO stats, string courseCode);
}