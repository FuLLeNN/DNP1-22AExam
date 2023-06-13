using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Student
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(25)]
    public string name { get; set; }
    [Required]
    public string programme { get; set; }
    
    //public ICollection<GradeInCourse> grades { get; set; }
}