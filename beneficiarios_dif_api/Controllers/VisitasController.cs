﻿using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/visitas")]
    [ApiController]
    public class VisitasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VisitasController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<VisitaDTO>> GetById(int id)
        {
            var visita = await context.Visitas
                .Include(b => b.Beneficiario)                
                .FirstOrDefaultAsync(v => v.Id == id);

            if (visita == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VisitaDTO>(visita));
        }

        private string GetBase64Image(string fileName)
        {
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);

            if (System.IO.File.Exists(filePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }

            return null;
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult> GetAll()
        {
            var visitas = await context.Visitas
                .Include(v => v.Beneficiario)
                    .ThenInclude(b => b.ProgramaSocial)
                .Include(v => v.Beneficiario)
                    .ThenInclude(b => b.Municipio)
                .ToListAsync();

            if (!visitas.Any())
            {
                return NotFound();
            }

            var visitasDTO = mapper.Map<List<VisitaDTO>>(visitas);

            foreach (var visita in visitasDTO)
            {
                visita.ImagenBase64 = GetBase64Image(visita.Foto); // Asigna el base64 de la imagen
            }

            return Ok(visitasDTO);
        }



        [HttpPost("crear")]
        public async Task<ActionResult> Post(VisitaDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            var visita = mapper.Map<Visita>(dto);
            visita.FechaHoraVisita = DateTime.Now;
            visita.Beneficiario = await context.Beneficiarios.SingleOrDefaultAsync(b => b.Id == dto.Beneficiario.Id);

            context.Visitas.Add(visita);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, VisitaDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var visita = await context.Visitas.FindAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                byte[] bytes = Convert.FromBase64String(dto.ImagenBase64);
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, bytes);
                dto.Foto = fileName;
            }

            mapper.Map(dto, visita);
            visita.Beneficiario = await context.Beneficiarios.SingleOrDefaultAsync(b => b.Id == dto.Beneficiario.Id);

            context.Update(visita);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Visitas.Any(e => e.Id == id))
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
        public async Task<ActionResult> Delete(int id)
        {
            var visita = await context.Visitas.FindAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            context.Visitas.Remove(visita);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("obtener-nube-palabras")]
        public async Task<ActionResult> WordCloud()
        {
            var comments = await context.Visitas.Select(v => v.Descripcion).ToListAsync();

            if (!comments.Any())
            {
                return NotFound();
            }

            var wordCount = CountWords(comments);
            return Ok(CreateModel(wordCount));
        }

        static Dictionary<string, int> CountWords(List<string> comments)
        {
            var words = comments.SelectMany(c => Regex.Matches(c.ToLower(), @"\b\w+\b").Select(match => match.Value)).ToList();

            var wordCount = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

            return wordCount;
        }

        static List<WordCloudDTO> CreateModel(Dictionary<string, int> wordCount)
        {
            return wordCount
                .Select(pair => new WordCloudDTO { Name = pair.Key, Weight = pair.Value })
                .OrderByDescending(modelo => modelo.Weight)
                .ToList();
        }

    }
}
