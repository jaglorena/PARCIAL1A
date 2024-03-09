﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibrosContext context;

        public LibrosController(LibrosContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("obtenerTodos")]
        public IActionResult Get()
        {
            List<Libros> libros = (from p in context.libros select p).ToList();

            if (libros.Count == 0) return NotFound();

            return Ok(libros);
        }

        [HttpGet]
        [Route("obtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            Libros? libros = (from p in context.libros
                            where p.Id == id
                            select p).FirstOrDefault();

            if (libros == null) return NotFound();

            return Ok(libros);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Libros? libro = (from p in context.libros
                           where p.Id == id
                           select p).FirstOrDefault();

            if (libro == null) return NotFound();

            context.libros.Attach(libro);
            context.libros.Remove(libro);
            context.SaveChanges();

            return Ok(libro);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] Libros libro)
        {
            Libros? libroActual = (from p in context.libros
                                 where p.Id == id
                                 select p).FirstOrDefault();

            if (libroActual == null) return NotFound();

            libroActual.Titulo = libro.Titulo;
            context.Entry(libroActual).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(libroActual);

        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult agregar([FromBody] Libros libro)
        {
            try
            {
                context.libros.Add(libro);
                context.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
