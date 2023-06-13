using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data;

public class DataAccess : IDataInterface
{
    private readonly StudentContext context;

    public DataAccess(StudentContext context)
    {
        this.context = context;
    }
    
    public async Task<Student> CreateStudentAsync(Student student)
    {
        Student toCreate = new Student()
        {
            name = student.name,
            programme = student.programme
        };

        EntityEntry<Student> newStudent = await context.students.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newStudent.Entity;
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        return await context.students.ToListAsync();
    }

    public async Task AddGradeToStudent(GradeInCourse g, int studentId)
    {
        //Check if there is already existing Grade to that student
        //If there is, change grade to new grade
        //If not, create a whole new grade
        GradeInCourse existingGrade = await context.grades.FirstOrDefaultAsync(grade =>
            grade.Student_Id == studentId && grade.courseCode == g.courseCode);
        if (existingGrade != null)
        {
            existingGrade.grade = g.grade;
        }
        else
        {
            GradeInCourse newGrade = new GradeInCourse()
            {
                grade = g.grade,
                courseCode = g.courseCode,
                Student_Id = g.Student_Id
            };
            await context.grades.AddAsync(newGrade);
        }

        await context.SaveChangesAsync();
    }

    public async Task<StatisticsOverviewDto> GetCourseStatisticsAsync(StatisticsDTO stats, string courseCode)
    {
        GradeInCourse course = await context.grades.FirstOrDefaultAsync(c => c.courseCode == courseCode);
        if (course == null)
        {
            throw new ArgumentException("Course not found");
        }

        StatisticsOverviewDto statistics = new StatisticsOverviewDto();
        statistics.courseCode = courseCode;

        if (stats.totalNumOfStudents)
            statistics.totalNumOfStudents = await context.grades.Where(g => g.courseCode == course.courseCode)
                .Select(g => g.Student_Id).Distinct().CountAsync();

        if (stats.totalNumOfPassedStudents)
            statistics.totalNumOfPassedStudents = await context.grades.Where(g => g.courseCode == course.courseCode && g.grade >= 2)
                .Select(g => g.Student_Id).Distinct().CountAsync();

        if (stats.avgGradeOverall)
            statistics.avgGradeOverall = (float?)await context.grades.Where(g => g.courseCode == course.courseCode)
                .AverageAsync(g => g.grade);

        if (stats.avgGradeOfPassed)
            statistics.avgGradeOfPassed = (float?)await context.grades
                .Where(g => g.courseCode == course.courseCode && g.grade >= 2).AverageAsync(g => g.grade);

        if (stats.medianGrade)
        {
            var grades = await context.grades.Where(g => g.courseCode == course.courseCode).Select(g => g.grade)
                .ToListAsync();
            grades.Sort();
            if (grades.Count % 2 != 0)
            {
                statistics.medianGrade = grades[grades.Count / 2];
            }
            else
            {
                statistics.medianGrade = grades[(int)Math.Ceiling(grades.Count/2.00)];
            }
        }

        return statistics;
    }
}