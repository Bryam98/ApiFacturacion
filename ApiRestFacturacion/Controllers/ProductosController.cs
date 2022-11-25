using ApiRestFacturacion.DTOS;
using ApiRestFacturacion.Models;
using ApiRestFacturacion.Models.Context;
using ApiRestFacturacion.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Controllers
{ 
    
    [Route("api/Productos")]
    [ApiController]
    public class ProductosController: ControllerBase
    {

        private readonly FacturacionDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IServiceTasaCambio serviceTasaCambio;

        public ProductosController(FacturacionDbContext dbContext, IMapper mapper, IServiceTasaCambio serviceTasaCambio)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.serviceTasaCambio = serviceTasaCambio;
        }

        [HttpGet("ListadoProductos")]
        public async Task<List<ProductoDTO>> ListadoProductos()
        {
            var productos = await dbContext.Producto.Take(10)
                                             .Where(x => x.Estado == true).ToListAsync();

            return mapper.Map<List<ProductoDTO>>(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> GetPorId([FromRoute] int id)
        {
            var producto = await dbContext.Producto.FirstOrDefaultAsync(x => x.IdProducto == id);

            if (producto is null)
            {
                return BadRequest("Error no existe producto con el id indicado");
            }

            return mapper.Map<ProductoDTO>(producto);

        }
        [HttpGet("GetPorNombre/{codigo}")]
        public async Task<ActionResult<ProductoDTO>> GetPorNombre([FromRoute] string codigo)
        {
            var producto = await dbContext.Producto.Include(x => x.TasaCambio).FirstOrDefaultAsync(x => x.Codigo.ToUpper().Equals(codigo.ToUpper().Trim()));

            if (producto is null)
            {
                return BadRequest("Error no existe ningun producto con el codigo indicado");
            }

            return mapper.Map<ProductoDTO>(producto);

        }
        [HttpGet("GetPorDescripcion/{descripcion}")]
        public async Task<ActionResult<ProductoDTO>> GetPorDescripcion([FromRoute] string descripcion)
        {
            var producto = await dbContext.Producto.Where(x => x.Descripcion.Contains(descripcion))
                                                                            .FirstOrDefaultAsync();

            if (producto is null)
            {
                return BadRequest("Error no existe ningun producto con la descripcion indicada");
            }

            return mapper.Map<ProductoDTO>(producto);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacionDTO)
        {

            var productoExiste = await dbContext.Producto.AnyAsync(x => x.Nombre == productoCreacionDTO.Nombre);

            if (productoExiste)
            {

                return BadRequest($"Ya existe el producto en la bd ");

            }

            var producto = mapper.Map<Producto>(productoCreacionDTO);

            var tasaCambioDia = await serviceTasaCambio.ObtenerTasaCambioDia();

            producto.IdTasa = tasaCambioDia.IdTasa;
            producto.PrecioDolar = producto.PrecioCordoba / tasaCambioDia.PrecioCambio;


            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPorId", new { id = producto.IdProducto }, productoCreacionDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ProductoCreacionDTO productoCreacionDTO)
        {
            var productoExiste = await dbContext.Producto.AnyAsync(x => x.Nombre == productoCreacionDTO.Nombre);

            if (!productoExiste)
            {

                return BadRequest($"No existe un producto con el id indicado");

            }

            var producto = mapper.Map<Producto>(productoCreacionDTO);
            var tasaCambio = await serviceTasaCambio.ObtenerTasaCambioPorId(id); 
           

            producto.IdProducto = id;
            producto.IdTasa = tasaCambio.IdTasa;
            producto.PrecioDolar = producto.PrecioCordoba / tasaCambio.PrecioCambio;

            dbContext.Update(producto);
            await dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> Delete (int id)
        {
            var productoExiste = await dbContext.Producto.AnyAsync(x => x.IdProducto == id);

            if (!productoExiste)
            {

                return BadRequest($"no existe un producto con el id indicado");

            }

            dbContext.Remove(new Producto(){ IdProducto = id});
            await dbContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
