using System.ComponentModel.DataAnnotations;

namespace CourseWorkMVC.Models
{
    public class Lesson
    { 
        public int Id { get; set; }

        public string? TeacherId { get; set; }

        [Display(Name = "Предмет")]
        public int? SubjectId { get; set; }

        [Display(Name = "Предмет")]
        public Subject? Subject { get; set; }

        public Account? Teacher { get; set; }

        [Display(Name = "Дата и время")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Аудитория")]
        public string? Audience { get; set; }

        [Display(Name = "Доп. информация")]
        public string? Description { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
        
        
        public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}
