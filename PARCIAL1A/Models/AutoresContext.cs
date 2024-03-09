using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class AutoresContext : DbContext
    {
        public AutoresContext(DbContextOptions<AutoresContext> options) : base(options) { }

    }
}
