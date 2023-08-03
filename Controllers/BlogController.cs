#nullable disable
using Microsoft.AspNetCore.Mvc;
using Saaya.Web.Common.Attributes;
using Saaya.Web.Db;
using Saaya.Web.Db.Models;
using Saaya.Web.Models;
using Saaya.Web.Utility;
using System.Data;
using System.Diagnostics;

namespace Saaya.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly SaayaWebContext _db;

        public BlogController(ILogger<BlogController> logger, SaayaWebContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult index()
        {
            return View(_db.Posts.OrderByDescending(x => x.DateCreated).ToList());
        }

        public IActionResult post(int id)
        {
            if (!_db.Posts.Where(x => x.Id == id).Any())
            { return RedirectToAction(nameof(index)); }

            return View(_db.Posts.Where(x => x.Id == id).FirstOrDefault());
        }

        [Authorized(Role = nameof(StaticDetails.Admin))]
        public IActionResult add(int? id)
        {
            Post post = new Post();

            if (id == null) { return View(post); }

            post = _db.Posts.FirstOrDefault(x => x.Id == id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorized(Role = nameof(StaticDetails.Admin))]
        public async Task<IActionResult> add(Post post)
        {
            if (ModelState.IsValid)
            {
                if (post.Id == 0) { _db.Posts.Add(post); }
                else { _db.Posts.Update(post); }

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(index));
            }
            else
            {
                if (post.Id != 0)
                {
                    post = _db.Posts.FirstOrDefault(x => x.Id == post.Id);
                }
            }

            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}