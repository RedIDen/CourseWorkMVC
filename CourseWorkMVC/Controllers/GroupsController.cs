#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using CourseWorkMVC.Data;
using CourseWorkMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseWorkMVC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static int? _groupId;

        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var context = _context.Group
                                  .Include(x => x.Students)
                                  .Include(x => x.Major)
                                  .OrderBy(x => x.BeginDate)
                                  .ThenBy(x => x.Name);
            return View(await context.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _groupId = id;
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                                       .Include(x => x.Major.Subjects)
                                       .Include(x => x.Students.OrderBy(x => x.LastName).ThenBy(x => x.UserName))
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            List<Major> allMajors = this._context.Major.ToList();
            ViewBag.AllMajors = new SelectList(allMajors, "Id", "Name");
            ViewData["MajorId"] = new SelectList(this._context.Major, "Id", "Id");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MajorId,BeginDate")] Group @group)
        {
            @group.Major = _context.Major.Find(@group.MajorId);

            if (ModelState.IsValid)
            {
                @group.Major = _context.Major.Find(@group.MajorId);
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Marks(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @subject = await _context.Subject.FindAsync(id);
            if (@subject == null)
            {
                return NotFound();
            }

            ViewBag.AllLessons =
                _context.Lesson.Where(x => x.TeacherId == StaticClass.CurrentAccount.Id && x.Subject == subject)
                        .OrderBy(x => x.DateTime);

            var students = _context.Account
                                   .Where(x => x.RoleId == 1 && x.Group.Id == _groupId)
                                   .OrderBy(x => x.LastName)
                                   .ThenBy(x => x.FirstName);
            ViewBag.AllStudents = students;

            ViewBag.AllMarks =
                _context.Mark.Where(x => x.Lesson.Subject == subject && students.Contains(x.Account));

            return View(@subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Marks()
        {
            int id = Convert.ToInt32(Request.Form["Id"].ToString());

            Subject subject = _context.Subject.Find(id);
            var students = _context.Account.Where(x => x.RoleId == 1 && x.Group.Id == _groupId);
            var lessons = _context.Lesson
                                  .Where(x => x.TeacherId == StaticClass.CurrentAccount.Id && x.Subject == subject)
                                  .OrderBy(x => x.DateTime);

            foreach (var student in students)
            {
                foreach (var lesson in lessons)
                {
                    string result = Request.Form[$"{student.Id}.{lesson.Id}"].ToString();
                    Mark mark = _context.Mark.FirstOrDefault(x => x.Account == student && x.Lesson == lesson);

                    if (!string.IsNullOrEmpty(result))
                    {
                        if (mark != null)
                        {
                            mark.Value = result;
                        }
                        else
                        {
                            _context.Mark.Add(new Mark()
                            {
                                Account = student,
                                Lesson = lesson,
                                Value = result,
                            });
                        }
                    }
                    else
                    {
                        if (mark != null)
                        {
                            _context.Mark.Remove(mark);
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new {id = _groupId});
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            List<Major> allMajors = this._context.Major.ToList();
            ViewBag.AllMajors = new SelectList(allMajors, "Id", "Name");
            ViewData["MajorId"] = new SelectList(this._context.Major, "Id", "Id");

            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BeginDate")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Group.FindAsync(id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }
    }
}