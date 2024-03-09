using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class AutorLibroContext : DbContext
    {
        public AutorLibroContext(DbContextOptions<AutorLibroContext> options) : base(options) { }

        public DbSet<AutorLibro> autorLibro { get; set; }

    }
}
