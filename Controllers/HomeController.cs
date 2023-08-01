using Microsoft.AspNetCore.Mvc;
using Saaya.Web.Models;
using System.Diagnostics;

namespace Saaya.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult index()
            => View();

        public IActionResult about()
            => View();

        public IActionResult privacy()
            => View();

        public IActionResult contact()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}