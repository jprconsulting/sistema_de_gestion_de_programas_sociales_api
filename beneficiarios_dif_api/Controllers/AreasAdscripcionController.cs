﻿using beneficiarios_dif_api.Entities;
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

        [HttpGet]
        public async Task<ActionResult<List<AreaAdscripcion>>> GetAreasAdscripcion()
        {
            return await context.AreasAdscripcion.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(AreaAdscripcion dto)
        {

            context.Add(dto);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}
