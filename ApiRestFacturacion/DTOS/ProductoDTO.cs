using ApiRestFacturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.DTOS
{
    public class ProductoDTO
    {
       
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCordoba { get; set; }
        public decimal PrecioDolar { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public int IdTasa { get; set; }
        public TasaCambio TasaCambio { get; set; }


    }
}
