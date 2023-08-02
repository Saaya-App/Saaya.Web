using Microsoft.AspNetCore.Mvc;
using Saaya.Web.Common.Extensions;
using Saaya.Web.Db;
using Saaya.Web.Services;

namespace Saaya.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly SaayaWebContext _db;
        private readonly DiscordLogin _discord;

        public AdminController(SaayaWebContext db, DiscordLogin discord)
        {
            _db = db;
            _discord = discord;
        }

        public IActionResult login()
            => Redirect(_discord.GetAuthURL());

        public async Task<IActionResult> callback(string code)
        {
            var token = await _discord.GetAccessToken(code);
            var user = await _discord.AuthenticateUser(token);

            if (user == null)
                return RedirectToAction("index", "home");

            HttpContext.Session.SetUInt64("UserId", user.Snowflake.Value);
            HttpContext.Session.SetString("UserRole", user.Role);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss tt zz")}] User {user.Snowflake} logged in.");
            Console.ResetColor();

            return RedirectToAction("index", "home");
        }
    }
}