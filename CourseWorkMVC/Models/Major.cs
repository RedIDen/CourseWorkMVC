using System.ComponentModel.DataAnnotations;

namespace CourseWorkMVC.Models
{
    public class Major
    {
        public int Id { get; set; }

        [Display(Name = "Специальность")]
        public string? Name { get; set; }

        [Display(Name = "Предметы")]
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
