using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/beneficiarios")]
    [ApiController]
    public class BeneficiariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BeneficiariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<Beneficiario>>> GetBeneficiarios()
        {
            return await context.Beneficiarios.ToListAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Create(BeneficiarioDTO dto)
        {
            var beneficiario = new Beneficiario
            {
                Nombres = dto.Nombres,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                FechaNacimiento = dto.FechaNacimiento,
                Domicilio = dto.Domicilio,
                Sexo = dto.Sexo,
                CURP = dto.CURP,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud,
                Estatus = dto.Estatus,
                MunicipioId = dto.MunicipioId,
                ProgramaSocialId = dto.ProgramaSocialId
            };

            context.Add(beneficiario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beneficiario = await context.Beneficiarios.FindAsync(id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            context.Beneficiarios.Remove(beneficiario);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Update(int id, BeneficiarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var beneficiario = await context.Beneficiarios.FindAsync(id);

            if (beneficiario == null)
            {
                return NotFound();
            }

            beneficiario.Nombres = dto.Nombres;
            beneficiario.ApellidoPaterno = dto.ApellidoPaterno;
            beneficiario.ApellidoMaterno = dto.ApellidoMaterno;
            beneficiario.FechaNacimiento = dto.FechaNacimiento;
            beneficiario.Domicilio = dto.Domicilio;
            beneficiario.Sexo = dto.Sexo;
            beneficiario.CURP = dto.CURP;
            beneficiario.Latitud = dto.Latitud;
            beneficiario.Longitud = dto.Longitud;
            beneficiario.Estatus = dto.Estatus;
            beneficiario.MunicipioId = dto.MunicipioId;
            beneficiario.ProgramaSocialId = dto.ProgramaSocialId;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiarioExists(id))
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

        private bool BeneficiarioExists(int id)
        {
            return context.Beneficiarios.Any(e => e.Id == id);
        }
    }
}
