using Data.Models;

namespace WebAPI.Data;

public interface IDataInterface
{
    Task<Student> CreateStudentAsync(Student student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task AddGradeToStudent(GradeInCourse grade, Student student);
    Task<StatisticsOverviewDto> GetCourseStatisticsAsync();
}