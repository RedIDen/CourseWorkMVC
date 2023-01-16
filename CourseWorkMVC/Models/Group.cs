using System.ComponentModel.DataAnnotations;

namespace CourseWorkMVC.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Display(Name = "Группа")]
        public string? Name { get; set; }

        [Display(Name = "Специальность")]
        public int? MajorId { get; set; }

        [Display(Name = "Специальность")]
        public Major? Major { get; set; }

        [Display(Name = "Дата начала учёбы")]
        public DateTime BeginDate { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        public ICollection<Account> Students { get; set; } = new List<Account>();
    }
}
