using Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPIStudent.Controllers;

[ApiController]
[Route("[controller]")]
public class GradeController : ControllerBase
{

    private readonly IDataInterface dataAccess;

    public GradeController(IDataInterface dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost, Route("statistics")]
    public async Task<ActionResult<StatisticsOverviewDto>> GetCourseStatisticsAsync(StatisticsDTO stats, string courseCode)
    {
        try
        {
            StatisticsOverviewDto statistics = await dataAccess.GetCourseStatisticsAsync(stats, courseCode);
            return Ok(statistics);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}