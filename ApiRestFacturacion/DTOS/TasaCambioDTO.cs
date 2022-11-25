using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.DTOS
{
    public class TasaCambioDTO
    {
        public int IdTasa { get; set; }
        public decimal PrecioCambio { get; set; }
        public DateTime Fecha { get; set; }



    }
}
