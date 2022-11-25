using ApiRestFacturacion.DTOS;
using ApiRestFacturacion.Models;
using ApiRestFacturacion.Models.Context;
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
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly FacturacionDbContext dbContext;
        private readonly IMapper mapper;

        public ClientesController(FacturacionDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet("ListadoClientes")]
        public async Task<List<ClienteDTO>> ListadoClientes()
        {
            var clientes = await dbContext.Cliente.Take(10).ToListAsync();

            return mapper.Map<List<ClienteDTO>>(clientes);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> GetPorId([FromRoute] int id)
        {
            var cliente = await dbContext.Cliente.FirstOrDefaultAsync(x => x.IdCliente == id);

            if (cliente is null)
            {
                return BadRequest("Error no existe ningun cliente con ese id");
            }

            return mapper.Map<ClienteDTO>(cliente);

        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<ClienteDTO>> GetPorNombre([FromRoute] string nombre)
        {
            var cliente = await dbContext.Cliente.FirstOrDefaultAsync(x => x.Nombres == nombre);

            if (cliente is null)
            {
                return BadRequest("Error no existe ningun cliente con ese nombre");
            }

            return mapper.Map<ClienteDTO>(cliente);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteCreacionDTO clienteCreacionDTO)
        {

            var clienteExiste = await dbContext.Cliente.AnyAsync(x => x.Cedula == clienteCreacionDTO.Cedula);

            if (clienteExiste)
            {

                return BadRequest($"Ya existe un cliente con el numero de cedula {clienteCreacionDTO.Cedula} en la bd ");

            }

            var cliente = mapper.Map<Cliente>(clienteCreacionDTO);

            dbContext.Add(cliente);
            await dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetPorId", new { id = cliente.IdCliente }, clienteCreacionDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, ClienteCreacionDTO clienteCreacionDTO)
        {
            var clienteExiste = await dbContext.Cliente.AnyAsync(x => x.IdCliente == id);

            if (!clienteExiste)
            {

                return BadRequest($"no existe un cliente con el id indicado");

            }

            var cliente = mapper.Map<Cliente>(clienteCreacionDTO);
            cliente.IdCliente = id;

            dbContext.Update(cliente);
            await dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteExiste = await dbContext.Cliente.AnyAsync(x => x.IdCliente == id);

            if (!clienteExiste)
            {

                return BadRequest($"no existe un cliente con el id indicado");

            }

            dbContext.Remove(new Cliente() { IdCliente = id});
            await dbContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
