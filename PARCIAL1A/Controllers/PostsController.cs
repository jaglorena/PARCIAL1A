using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsContext context;

        public PostsController(PostsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("obtenerTodos")]
        public IActionResult Get()
        {
            List<Posts> posts = (from p in context.posts select p).ToList();

            if(posts.Count == 0) return NotFound();

            return Ok(posts);
        }

        [HttpGet]
        [Route("obtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            Posts? posts = (from p in context.posts 
                           where p.Id == id
                           select p).FirstOrDefault();

            if (posts == null) return NotFound();

            return Ok(posts);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Posts? post = (from p in context.posts
                            where p.Id == id
                            select p).FirstOrDefault();

            if (post == null) return NotFound();

            context.posts.Attach(post);
            context.posts.Remove(post);
            context.SaveChanges();

            return Ok(post);
        }
    }
}
