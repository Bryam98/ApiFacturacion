using ApiRestFacturacion.DTOS;
using ApiRestFacturacion.Models;
using ApiRestFacturacion.Models.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Controllers
{
    [Route("api/Tasacambio")]
    [ApiController]
    public class TasaCambioController : ControllerBase
    {
        private readonly FacturacionDbContext dbContext;
        private readonly IMapper mapper;

        public TasaCambioController(FacturacionDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        [HttpGet("GetPorMes")]
        public async Task<ActionResult<List<TasaCambioDTO>>> GetPorMes(DateTime mes)
        {
            var mesBuscar = mes.Month;

            var tasaCambioMes = await dbContext.TasaCambio.Where(x => x.Fecha.Month == mesBuscar).ToListAsync();
                                            

        
            if (tasaCambioMes is null)
            {
                return NotFound($"El {mes} no tiene tasas de cambio registradas");
            }

            return mapper.Map<List<TasaCambioDTO>>(tasaCambioMes);

        }


        [HttpGet]
        public async Task<ActionResult<TasaCambioDTO>> GetPorDia()
        {
            DateTime FechaDeDia = DateTime.Today;

            var tasaCambioDia = await dbContext.TasaCambio.FirstOrDefaultAsync(x => x.Fecha == FechaDeDia);

            if (tasaCambioDia is null)
            {
                return NotFound("A un no se ingresado tasa de cambio del dia");
            }

            return mapper.Map<TasaCambioDTO>(tasaCambioDia);

        }

        [HttpGet("{id:int}", Name = "GetPorId")]
        public async Task<ActionResult<TasaCambioDTO>> GetPorId([FromRoute] int id)
        {

            var tasaCambioExiste = await dbContext.TasaCambio.FirstOrDefaultAsync(x => x.IdTasa == id);

            if (tasaCambioExiste is null)
            {
                return NotFound("No existe una tasa de cambio con el Id indicado");
            }

            return mapper.Map<TasaCambioDTO>(tasaCambioExiste);

        }



        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TasaCambioCreacionDTO tasaCambioCreacionDTO)
        {

            var tasaCambio = mapper.Map<TasaCambio>(tasaCambioCreacionDTO);
            tasaCambio.Fecha = DateTime.Today;

            dbContext.Add(tasaCambio);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPorId", new { id = tasaCambio.IdTasa }, tasaCambioCreacionDTO);
        }

        //Implementamos el metodo HTTP PATCH
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TasaCambioPatchDTO> patchDocument)
        {

            if (patchDocument == null)
            {

                return BadRequest();
            }


            //Luego traeremos el libro que el cliente quiere actulizar la busqueda 
            //sera conforme el Id recibido en la peticion.
            var tasaCambio = await dbContext.TasaCambio
                .FirstOrDefaultAsync(x => x.IdTasa == id);


            if (tasaCambio == null)
            {
                return BadRequest();
            }

            
            var tasaCambioPatchDTO = mapper.Map<TasaCambioPatchDTO>(tasaCambio);

            patchDocument.ApplyTo(tasaCambioPatchDTO, ModelState);

            var isValido = TryValidateModel(tasaCambioPatchDTO);

            if (!isValido)
            {
                return BadRequest(ModelState);
            }


            //aplicando los cambios.
            tasaCambio = mapper.Map(tasaCambioPatchDTO, tasaCambio);

          
            await dbContext.SaveChangesAsync();

            //retornamos un 204 la solicitud fue realizada correctamente
            return NoContent();

        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> Delete(int id)
        {
            var TasaCambioExiste = await dbContext.TasaCambio.AnyAsync(x => x.IdTasa == id);

            if (!TasaCambioExiste)
            {

                return BadRequest($"Error no existe nigun registro que el id indicado");

            }

            dbContext.Remove(new TasaCambio (){ IdTasa = id});
            await dbContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
