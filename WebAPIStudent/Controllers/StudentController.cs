using Data.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;

namespace WebAPIStudent.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{

    private readonly IDataInterface dataAccess;

    public StudentController(IDataInterface dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    [HttpPost,Route("add")]
    public async Task<ActionResult> CreateAsync([FromBody] Student student)
    {
        try
        {
            Student newStudent = await dataAccess.CreateStudentAsync(student);
            return Created($"/user/{student.id}", newStudent);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet, Route("getAll")]
    public async Task<ActionResult<ICollection<Student>>> GetAllStudentsAsync()
    {
        try
        {
            ICollection<Student> studentsList = await dataAccess.GetAllStudentsAsync();
            return Ok(studentsList);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("addGradeToStudent")]
    public async Task<ActionResult> AddGradeToStudentAsync([FromBody] GradeInCourse grade, int studentId)
    {
        try
        {
            await dataAccess.AddGradeToStudent(grade, studentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}