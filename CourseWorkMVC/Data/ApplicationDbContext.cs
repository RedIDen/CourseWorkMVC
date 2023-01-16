using CourseWorkMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.Account>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static ApplicationDbContext? Instance { get; set; }

        public DbSet<Lesson> Lesson { get; set; }

        public DbSet<Mark> Mark { get; set; }

        public DbSet<Group> Group { get; set; }

        public DbSet<Major> Major { get; set; }

        public DbSet<Subject> Subject { get; set; }

        public DbSet<Models.Account> Account { get; set; }
    }
}