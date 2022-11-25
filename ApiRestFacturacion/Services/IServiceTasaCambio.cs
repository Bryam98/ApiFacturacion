using ApiRestFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Services
{
    public interface IServiceTasaCambio
    {
        Task<TasaCambio> ObtenerTasaCambioDia();
        Task<TasaCambio> ObtenerTasaCambioPorId(int id);
    }
}
