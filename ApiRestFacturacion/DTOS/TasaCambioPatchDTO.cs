using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.DTOS
{
    public class TasaCambioPatchDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(6,4)")]
        public decimal PrecioCambio { get; set; }

    }
}
