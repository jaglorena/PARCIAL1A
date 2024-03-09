using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly AutorLibroContext context;

        public AutorLibroController(AutorLibroContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("obtenerTodos")]
        public IActionResult Get()
        {
            List<AutorLibro> librosPorAutor = (from p in context.autorLibros select p).ToList();

            if (librosPorAutor.Count == 0) return NotFound();

            return Ok(librosPorAutor);
        }

        [HttpGet]
        [Route("obtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            AutorLibro? librosPorAutor = (from p in context.autorLibros
                            where p.LibroId == id
                            select p).FirstOrDefault();

            if (librosPorAutor == null) return NotFound();

            return Ok(librosPorAutor);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            AutorLibro? librosPorAutor = (from p in context.autorLibros
                           where p.LibroId == id
                           select p).FirstOrDefault();

            if (librosPorAutor == null) return NotFound();

            context.autorLibros.Attach(librosPorAutor);
            context.autorLibros.Remove(librosPorAutor);
            context.SaveChanges();

            return Ok(librosPorAutor);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] AutorLibro libroPorAutor)
        {
            AutorLibro? libroPorAutorActual = (from p in context.autorLibros
                                 where p.LibroId == id
                                 select p).FirstOrDefault();

            if (libroPorAutorActual == null) return NotFound();

            libroPorAutorActual.Orden = libroPorAutor.Orden;
            context.Entry(libroPorAutorActual).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(libroPorAutorActual);

        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult agregar([FromBody] AutorLibro libroPorAutor)
        {
            try
            {
                context.autorLibros.Add(libroPorAutor);
                context.SaveChanges();
                return Ok(libroPorAutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
