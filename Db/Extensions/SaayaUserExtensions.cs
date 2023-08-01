#nullable disable
using Microsoft.EntityFrameworkCore;
using Saaya.Web.Db.Models;

namespace Saaya.Web.Db.Extensions
{
    public static class SaayaUserExtensions
    {
        public static bool UserExists(this DbSet<SaayaUser> users, string token)
            => users.Any(x => x.Token == token);

        public static IQueryable<SaayaUser> Include(this DbSet<SaayaUser> users)
            => users.Include(x => x.Songs)
                    .Include(x => x.Playlists)
                    .AsNoTracking()
                    .AsQueryable();

        public static List<Song>? GetSongs(this DbSet<SaayaUser> user, string token)
            => Include(user)
                .Where(x => x.Token == token)
                .FirstOrDefault()
                .Songs
                .ToList();

        public static List<Playlist>? GetPlaylists(this DbSet<SaayaUser> user, string token)
            => Include(user)
                .Where(x => x.Token == token)
                .FirstOrDefault()
                .Playlists
                .ToList();

        public static List<Song>? GetPlaylistSongs(this DbSet<SaayaUser> user, string token, int playlist)
            => Include(user)
                .Where(x => x.Token == token)
                .FirstOrDefault()
                .Songs
                .Where(x => x.PlaylistId == playlist)
                .ToList();
    }
}