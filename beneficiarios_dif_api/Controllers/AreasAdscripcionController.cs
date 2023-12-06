using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/areas-adscripcion")]
    [ApiController]
    public class AreasAdscripcionController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AreasAdscripcionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<AreaAdscripcion>>> GetAreasAdscripcion()
        {
            return await context.AreasAdscripcion.ToListAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Create(AreaAdscripcionDTO dto)
        {
            var areaAdscripcion = new AreaAdscripcion
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Estatus = dto.Estatus
            };

            context.Add(areaAdscripcion);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var areaAdscripcion = await context.AreasAdscripcion.FindAsync(id);

            if (areaAdscripcion == null)
            {
                return NotFound();
            }

            context.AreasAdscripcion.Remove(areaAdscripcion);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Update(int id, AreaAdscripcionDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var areaAdscripcion = await context.AreasAdscripcion.FindAsync(id);

            if (areaAdscripcion == null)
            {
                return NotFound();
            }

            areaAdscripcion.Nombre = dto.Nombre;
            areaAdscripcion.Descripcion = dto.Descripcion;
            areaAdscripcion.Estatus = dto.Estatus;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaAdscripcionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AreaAdscripcionExists(int id)
        {
            return context.AreasAdscripcion.Any(e => e.Id == id);
        }
    }
}
