using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models
{
    public class PostsContext : DbContext
    {
        public PostsContext(DbContextOptions<PostsContext> options) : base(options) { }

    }
}
