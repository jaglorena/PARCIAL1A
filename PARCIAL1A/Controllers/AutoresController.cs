using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AutoresContext context;

        public AutoresController(AutoresContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("obtenerTodos")]
        public IActionResult Get()
        {
            List<Autores> autores = (from p in context.autores select p).ToList();

            if (autores.Count == 0) return NotFound();

            return Ok(autores);
        }

        [HttpGet]
        [Route("obtenerPorId/{id}")]
        public IActionResult Get(int id)
        {
            Autores? autores = (from p in context.autores
                            where p.Id == id
                            select p).FirstOrDefault();

            if (autores == null) return NotFound();

            return Ok(autores);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            Autores? autores = (from p in context.autores
                           where p.Id == id
                           select p).FirstOrDefault();

            if (autores == null) return NotFound();

            context.autores.Attach(autores);
            context.autores.Remove(autores);
            context.SaveChanges();

            return Ok(autores);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] Autores autores)
        {
            Autores? autorActual = (from p in context.autores
                                 where p.Id == id
                                 select p).FirstOrDefault();

            if (autorActual == null) return NotFound();

            autorActual.Nombre = autores.Nombre;
            context.Entry(autorActual).State = EntityState.Modified;
            context.SaveChanges();

            return Ok(autorActual);

        }

        [HttpPost]
        [Route("agregar")]
        public IActionResult agregar([FromBody] Autores autores)
        {
            try
            {
                context.autores.Add(autores);
                context.SaveChanges();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
