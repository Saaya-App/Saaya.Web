using Microsoft.EntityFrameworkCore;
using Saaya.Web.Db.Models;

namespace Saaya.Web.Db
{
    public class SaayaWebContext : DbContext
    {
        public DbSet<SaayaUser> Users { get; set; }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        public SaayaWebContext(DbContextOptions<SaayaWebContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlist>()
                .HasOne(x => x.User)
                .WithMany(x => x.Playlists)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Song>()
                .HasOne(x => x.User)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.UserId);
        }
    }
}