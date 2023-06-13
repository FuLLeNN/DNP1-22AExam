using Data.Models;

namespace WebAPI.Data;

public interface IDataInterface
{
    Task<Student> CreateStudentAsync(Student student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task AddGradeToStudent(GradeInCourse grade, int studentId);
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync(StatisticsDTO stats, string courseId);
}