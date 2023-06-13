using System.Text;
using System.Text.Json;
using Data.Models;

namespace Blazor.Data;

public class StudentHttpClient : IStudentService
{

    private readonly HttpClient client;

    public StudentHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task CreateAsync(Student student)
    {
        string jsonStudent = JsonSerializer.Serialize(student);
        StringContent content = new(jsonStudent, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:5236/Student/add", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<ICollection<Student>> GetAllStudentsAsync()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:5236/Student/getAll");
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        ICollection<Student> students =
            JsonSerializer.Deserialize<ICollection<Student>>(responseContent, new JsonSerializerOptions());
        return students;
    }

    public async Task AddGradeToStudentAsync(GradeInCourse grade, int studentId)
    {
        string jsonGrade = JsonSerializer.Serialize(grade);
        StringContent content = new(jsonGrade, Encoding.UTF8, "application/json");

        HttpResponseMessage response =
            await client.PostAsync("http://localhost:5236/Student/addGradeToStudent?studentId=" + studentId, content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}