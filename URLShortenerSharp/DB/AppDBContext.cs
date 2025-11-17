using Microsoft.EntityFrameworkCore;
using URLShortenerSharp.Models;

namespace URLShortenerSharp.DB
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<URL> URLs { get; set; }
    }
}
