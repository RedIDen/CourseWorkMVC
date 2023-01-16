using System.ComponentModel.DataAnnotations;

namespace CourseWorkMVC.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name = "Предмет")]

        public string? Name { get; set; }

        [Display(Name = "Специальности")]
        public ICollection<Major> Majors { get; set; } = new List<Major>();

        public ICollection<Account> Teachers { get; set; } = new List<Account>();
    }
}
