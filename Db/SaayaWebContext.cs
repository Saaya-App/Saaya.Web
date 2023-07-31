using Microsoft.EntityFrameworkCore;

namespace Saaya.Web.Db
{
    public class SaayaWebContext : DbContext
    {
        public SaayaWebContext(DbContextOptions<SaayaWebContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // To Do
        }
    }
}