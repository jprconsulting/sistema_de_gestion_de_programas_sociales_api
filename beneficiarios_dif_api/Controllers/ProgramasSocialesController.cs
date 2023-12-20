using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/programas-sociales")]
    [ApiController]
    public class ProgramasSocialesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProgramasSocialesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ProgramaSocial>>> GetProgramasSociales()
        {
            return await context.ProgramasSociales.ToListAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Create(ProgramaSocialDTO dto)
        {
            //var programaSocial = new ProgramaSocial
            //{
            //    Nombre = dto.Nombre,
            //    Descripcion = dto.Descripcion,
            //    Color = dto.Color,
            //    Estatus = dto.Estatus,
            //    AreaAdscripcionId = dto.AreaAdscripcionId,
            //    Acronimo = dto.Acronimo
            //};

            //context.Add(programaSocial);
            //await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var programaSocial = await context.ProgramasSociales.FindAsync(id);

            if (programaSocial == null)
            {
                return NotFound();
            }

            context.ProgramasSociales.Remove(programaSocial);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Update(int id, ProgramaSocialDTO dto)
        {
            //if (id != dto.Id)
            //{
            //    return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            //}

            //var programaSocial = await context.ProgramasSociales.FindAsync(id);

            //if (programaSocial == null)
            //{
            //    return NotFound();
            //}

            //programaSocial.Nombre = dto.Nombre;
            //programaSocial.Descripcion = dto.Descripcion;
            //programaSocial.Color = dto.Color;
            //programaSocial.Estatus = dto.Estatus;
            //programaSocial.AreaAdscripcionId = dto.AreaAdscripcionId;
            //programaSocial.Acronimo = dto.Acronimo;

            //try
            //{
            //    await context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProgramaSocialExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        private bool ProgramaSocialExists(int id)
        {
            return context.ProgramasSociales.Any(e => e.Id == id);
        }
    }
}
