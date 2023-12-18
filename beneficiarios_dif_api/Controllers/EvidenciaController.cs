using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/evidencias")]
    [ApiController]
    public class EvidenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EvidenciasController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EvidenciaDTO>> GetById(int id)
        {
            var evidencia = await _context.Evidencias.FindAsync(id);

            if (evidencia == null)
            {
                return NotFound();
            }

            return _mapper.Map<EvidenciaDTO>(evidencia);
        }

        [HttpGet("obtener-todas")]
        public async Task<ActionResult> GetAllEvidencias()
        {
            var evidencias = await _context.Evidencias.ToListAsync();
            return Ok(_mapper.Map<EvidenciaDTO[]>(evidencias));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> CreateEvidencia(EvidenciaDTO evidenciaDTO)
        {
            if (!string.IsNullOrEmpty(evidenciaDTO.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(evidenciaDTO.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                evidenciaDTO.Foto = fileName;
            }

            var evidencia = _mapper.Map<Evidencia>(evidenciaDTO);

            _context.Evidencias.Add(evidencia);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> UpdateEvidencia(int id, EvidenciaDTO evidenciaDTO)
        {
            if (id != evidenciaDTO.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var evidencia = await _context.Evidencias.FindAsync(id);

            if (evidencia == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(evidenciaDTO.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(evidenciaDTO.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                evidenciaDTO.Foto = fileName;
            }

            _mapper.Map(evidenciaDTO, evidencia);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Evidencias.Any(e => e.Id == id))
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

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> DeleteEvidencia(int id)
        {
            var evidencia = await _context.Evidencias.FindAsync(id);

            if (evidencia == null)
            {
                return NotFound();
            }

            _context.Evidencias.Remove(evidencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
