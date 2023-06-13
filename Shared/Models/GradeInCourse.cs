using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

public class GradeInCourse
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(4)]
    public string courseCode { get; set; }
    [Required]
    public int grade { get; set; }
    [ForeignKey("Student")]
    public int Student_Id { get; set; }
    
}