using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/indicadores")]
    [ApiController]
    public class IndicadoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IndicadoresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("obtener/{id:int}")]
        public async Task<ActionResult<IndicadoresDTO>> GetById(int id)
        {
            var indicador = await _context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            return _mapper.Map<IndicadoresDTO>(indicador);
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<IndicadoresDTO>>> GetIndicadores()
        {
            var indicadores = await _context.Indicadores.ToListAsync();
            return _mapper.Map<List<IndicadoresDTO>>(indicadores);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(IndicadoresDTO dto)
        {
            var indicador = _mapper.Map<Indicadores>(dto);

            _context.Indicadores.Add(indicador);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var indicador = await _context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            _context.Indicadores.Remove(indicador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] IndicadoresDTO dto)
        {
            if (id != dto.IndicadorId)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var indicador = await _context.Indicadores.FindAsync(id);

            if (indicador == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, indicador);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicadorExists(id))
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

        private bool IndicadorExists(int id)
        {
            return _context.Indicadores.Any(e => e.IndicadorId == id);
        }
    }
}
