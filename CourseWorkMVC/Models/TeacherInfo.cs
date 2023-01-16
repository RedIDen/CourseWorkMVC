namespace CourseWorkMVC.Models
{
    public class TeacherInfo
    {
        public int Id { get; set; }

        public int? AccountId { get; set; }

        public Account? Account { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
