#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseWorkMVC.Data;

namespace CourseWorkMVC.Controllers
{
    public class MainController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Main
        public async Task<IActionResult> Index()
        {
            StaticClass.CurrentAccount.Group = _context.Group.Find(StaticClass.CurrentAccount.GroupId);
            var applicationDbContext = _context.Lesson
                .Where(x => x.Groups.Contains(StaticClass.CurrentAccount.Group) && x.DateTime > DateTime.Now.AddHours(-2))
                .OrderBy(x => x.DateTime)
                .Include(l => l.Groups)
                .Include(l => l.Subject)
                .Include(l => l.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Subjects()
        {
            StaticClass.CurrentAccount.Group = _context.Group.Find(StaticClass.CurrentAccount.GroupId);
            Major major = _context.Major.Find(StaticClass.CurrentAccount.Group.MajorId);

            return View(_context.Subject.Where(x => x.Majors.Contains(major)));
        }

        public async Task<IActionResult> Marks(int? id)
        {
            StaticClass.CurrentAccount.Group = _context.Group.Find(StaticClass.CurrentAccount.GroupId);
            if (id == null)
            {
                return NotFound();
            }

            var marks = await _context.Mark
                .Where(x => x.Account.Id == StaticClass.CurrentAccount.Id && x.Lesson.SubjectId == id)
                .Include(x => x.Lesson)
                .Include(x => x.Lesson.Teacher)
                .OrderBy(x => x.Lesson.DateTime)
                .ToListAsync();


            return View(marks);
        }

        public async Task<IActionResult> Teachers()
        {
            StaticClass.CurrentAccount.Group = _context.Group.Find(StaticClass.CurrentAccount.GroupId);
            return View(await _context.Account.Where(x => x.RoleId == 2).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync());
        }

        public async Task<IActionResult> TeachersLessons(string? id)
        {
            StaticClass.CurrentAccount.Group = _context.Group.Find(StaticClass.CurrentAccount.GroupId);
            Account teacher = _context.Account.Find(id);
            ViewBag.Name = $"{teacher.LastName} {teacher.FirstName} {teacher.SurName}";
            var applicationDbContext = _context.Lesson
                .Where(x => x.Teacher == teacher && x.DateTime > DateTime.Now)
                .OrderBy(x => x.DateTime)
                .Include(l => l.Groups)
                .Include(l => l.Subject)
                .Include(l => l.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }
    }
}
