using Microsoft.AspNetCore.Mvc;

namespace Saaya.Web.Controllers
{
    public class ApiController : Controller
    {
        public ApiController()
        {
            
        }

        public IActionResult docs()
            => View();
    }
}