using ApiRestFacturacion.Models;
using ApiRestFacturacion.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Services
{
    public class ServiceTasaCambio:IServiceTasaCambio
    {
        private readonly FacturacionDbContext dbContext;

        public ServiceTasaCambio(FacturacionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TasaCambio> ObtenerTasaCambioDia()
        {
            var fechaHoy = DateTime.Today;

            var tasaCambio = await dbContext.TasaCambio.
                FirstOrDefaultAsync(x => x.Fecha == fechaHoy);
                
       
            return tasaCambio;

        }

        public async Task<TasaCambio> ObtenerTasaCambioPorId(int id)
        {
            var tasaCambio = await dbContext.TasaCambio.FirstOrDefaultAsync(x => x.IdTasa == id);

            return tasaCambio;
        }
    }
}
