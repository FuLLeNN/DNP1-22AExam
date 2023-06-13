using Data.Models;

namespace Blazor.Data;

public interface IStudentService
{
    Task CreateAsync(Student student);
    Task<ICollection<Student>> GetAllStudentsAsync();
    Task AddGradeToStudentAsync(GradeInCourse grade, int studentId);
}