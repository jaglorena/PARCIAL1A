using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class LibrosContext : DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options) : base(options) { }

        public DbSet<Libros> libros { get; set; }
    }
}
