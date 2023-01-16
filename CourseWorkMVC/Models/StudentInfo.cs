namespace CourseWorkMVC.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }
        
        public int? AccountId { get; set; }

        public Account? Account { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Surname { get; set; }

        public int? GroupId { get; set; }

        public Group? Group { get; set; }
    }
}
