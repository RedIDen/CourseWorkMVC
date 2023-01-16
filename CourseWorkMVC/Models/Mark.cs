namespace CourseWorkMVC.Models
{
    public class Mark
    {
        public int Id { get; set; }

        public string? StudentId { get; set; }

        public Account? Account { get; set; }

        public int? LessonId { get; set; }

        public Lesson? Lesson { get; set; }

        public string? Value { get; set; }
    }
}
