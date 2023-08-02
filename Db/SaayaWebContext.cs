using Microsoft.EntityFrameworkCore;
using Saaya.Web.Db.Models;

namespace Saaya.Web.Db
{
    public class SaayaWebContext : DbContext
    {
        public DbSet<SaayaUser> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SaayaWebContext(DbContextOptions<SaayaWebContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}