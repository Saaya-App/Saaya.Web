using Microsoft.AspNetCore.Mvc;
using Saaya.Web.Db;
using Saaya.Web.Db.Extensions;
using Saaya.Web.Db.Models;
using Saaya.Web.Models;
using System.Diagnostics;

namespace Saaya.Web.Controllers
{
    [Route("[controller]")]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;
        private readonly SaayaWebContext _db;

        public ApiController(ILogger<ApiController> logger, SaayaWebContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        [Route("songs/")]
        public IActionResult GetSongsForDevice()
        {
            string AuthToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (string.IsNullOrEmpty(AuthToken))
                return BadRequest(new List<Song>());

            if (!_db.Users.UserExists(AuthToken))
                return BadRequest(new List<Song>());

            return Ok(_db.Users.GetSongs(AuthToken) ?? new List<Song>());
        }

        [HttpGet]   
        [Route("playlists/")]
        public IActionResult GetPlaylistsForDevice()
        {
            string AuthToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (string.IsNullOrEmpty(AuthToken))
                return BadRequest(new List<Playlist>());

            if (!_db.Users.UserExists(AuthToken))
                return BadRequest(new List<Playlist>());

            return Ok(_db.Users.GetPlaylists(AuthToken) ?? new List<Playlist>());
        }

        [HttpGet]
        [Route("playlists/songs")]
        public IActionResult GetAllSongsForPlaylist(int playlist)
        {
            string AuthToken = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (string.IsNullOrEmpty(AuthToken))
                return BadRequest(new List<Song>());

            if (!_db.Users.UserExists(AuthToken))
                return BadRequest(new List<Song>());

            return Ok(_db.Users.GetPlaylistSongs(AuthToken, playlist) ?? new List<Song>());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}