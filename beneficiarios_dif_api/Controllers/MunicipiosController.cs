using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beneficiarios_dif_api.Controllers
{
    [Route("api/municipios")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MunicipiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-indicador")]
        public async Task<ActionResult<List<TotalBeneficiariosMunicipioDTO>>> GetMunicipiosConBeneficiariosYColores()
        {
            try
            {
                var municipios = await context.Municipios.Include(m => m.Beneficiarios).ToListAsync();
                var indicadores = await context.Indicadores.ToListAsync();

                var municipiosDTO = municipios.Select(m =>
                {
                    var totalBeneficiarios = m.Beneficiarios.Count;
                    var indicador = indicadores.FirstOrDefault(i => totalBeneficiarios >= i.RangoInicial && totalBeneficiarios <= i.RangoFinal);
                    var color = indicador != null ? indicador.Color : "#FFFFFF";
                    var descripcionIndicador = indicador != null ? indicador.Descripcion : "Sin descripción";

                    return new TotalBeneficiariosMunicipioDTO
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        TotalBeneficiarios = totalBeneficiarios,
                        Color = color,
                        DescripcionIndicador = descripcionIndicador
                    };
                }).ToList();

                return municipiosDTO;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener municipios con beneficiarios y colores: {ex.Message}");
            }
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<MunicipioDTO>>> GetMunicipios()
        {
            var municipios = await context.Municipios.ToListAsync();
            return mapper.Map<List<MunicipioDTO>>(municipios);
        }
    }
}
