﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsContext context;
        private readonly Parcial1aContext parcial1AContext;

        public PostsController(PostsContext context,  Parcial1aContext parcial1AContext)
        {
            this.context = context;
            this.parcial1AContext = parcial1AContext;
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

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] Posts post) {
            Posts? postActual = (from p in context.posts
                           where p.Id == id
                           select p).FirstOrDefault();

            if (postActual == null) return NotFound();

            postActual.Titulo = post.Titulo;
            postActual.Contenido = post.Contenido;
            context.Entry(postActual).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(postActual);

        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult agregar([FromBody] Posts posts) {
            try
            {
                context.posts.Add(posts);
                context.SaveChanges();
                return Ok(posts);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listado")]
        public IActionResult listado()
        {
            var ListadoPost = (from p in parcial1AContext.Posts
                                 join a in parcial1AContext.Autores
                                    on p.AutorId equals a.Id
                                    
                                    select new
                                    {
                                        p.Id,
                                        p.Titulo,
                                        a.Nombre

                                    })
                                    .Take(20).OrderByDescending(nombre => nombre.Nombre).ToList();

            if(ListadoPost.Count() == 0)
            {
                return NotFound();
            }

            return Ok(ListadoPost);


        }

    }
}
