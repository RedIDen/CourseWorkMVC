using Microsoft.AspNetCore.Identity;

namespace CourseWorkMVC.Models
{
    public class Account : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? SurName { get; set; }

        public int? GroupId { get; set; }

        public Group? Group { get; set; }

        public int RoleId { get; set; } = 1;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

        public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    }
}
