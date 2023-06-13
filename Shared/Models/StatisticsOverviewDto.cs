using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class StatisticsOverviewDto
{
    public string courseCode { get; set; }
    public int? totalNumOfPassedStudents{ get; set; }
    public int? totalNumOfStudents{ get; set; }
    public float? avgGradeOverall{ get; set; }
    public float? avgGradeOfPassed{ get; set; }
    public int? medianGrade{ get; set; }
}