using Data.Models;

namespace WebAPI.Data;

public class DataAccess : IDataInterface
{
    public Task<Student> CreateStudentAsync(Student student)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Student>> GetAllStudentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddGradeToStudent(GradeInCourse grade, Student student)
    {
        throw new NotImplementedException();
    }

    public Task<StatisticsOverviewDto> GetCourseStatisticsAsync()
    {
        throw new NotImplementedException();
    }
}