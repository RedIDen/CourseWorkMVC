using CourseWorkMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CourseWorkMVC.Data;
using Microsoft.AspNetCore.Authorization;

namespace CourseWorkMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (StaticClass.CurrentAccount == null)
            {
                return View();
            }

            if (StaticClass.CurrentAccount.RoleId == 2)
            {
                return RedirectToAction(nameof(Index), "Lessons");
            }
            else
            {
                return RedirectToAction(nameof(Index), "Main");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}