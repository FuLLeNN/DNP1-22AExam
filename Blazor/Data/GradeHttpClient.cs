using System.Text;
using System.Text.Json;
using Data.Models;

namespace Blazor.Data;

public class GradeHttpClient : IGradeService
{
    private readonly HttpClient client;

    public GradeHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(StatisticsDTO stats, string courseCode)
    {
        string statsJson = JsonSerializer.Serialize(stats);
        StringContent content = new(statsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response =
            await client.PostAsync("http://localhost:5236/Grade/statistics?courseCode=" + courseCode, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        StatisticsOverviewDto statisticsOverviewDto =
            JsonSerializer.Deserialize<StatisticsOverviewDto>(responseContent, new JsonSerializerOptions());
        return statisticsOverviewDto;
    }
}