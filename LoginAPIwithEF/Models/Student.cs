using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAPIwithEF.Models
{
    //Student(entity model)
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string? Name { get; set; }
     
        public string? Phone { get; set; }

        public string? Password { get; set; }
    }
}
