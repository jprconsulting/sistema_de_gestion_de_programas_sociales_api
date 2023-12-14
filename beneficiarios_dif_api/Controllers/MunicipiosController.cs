using AutoMapper;
using beneficiarios_dif_api.DTOs;
using beneficiarios_dif_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<ActionResult<List<MunicipioDTO>>> GetMunicipiosConBeneficiarios()
        {
            try
            {
                var municipios = await context.Municipios.Include(m => m.Beneficiarios).ToListAsync();

                var municipiosDTO = municipios.Select(m =>
                    new MunicipioDTO
                    {
                        Id = m.Id,
                        Nombre = m.Nombre,
                        TotalBeneficiarios = m.Beneficiarios.Count 
                    }).ToList();

                return municipiosDTO;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener municipios con beneficiarios: {ex.Message}");
            }
        }
    }
}

