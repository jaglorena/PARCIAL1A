using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class Parcial1aContext : DbContext
    {
        public Parcial1aContext(DbContextOptions<Parcial1aContext> options) : base(options)
        {

        }

        public DbSet<Autores> Autores { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<Libros> Libros { get; set; }
    }
}
