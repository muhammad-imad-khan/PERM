using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PERM.Constants;
using PERM.Models;
using System.Diagnostics;

namespace PERM.Controllers
{
    [Authorize(Roles = "SuperAdmin,PowerUser,HrAdmin ,BasicUser, ProjectManager_TeamLead")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}