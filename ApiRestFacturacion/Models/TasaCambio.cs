using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Models
{
    public class TasaCambio
    {
        [Key]
        public int IdTasa { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(6,4)")]
        public decimal PrecioCambio { get; set; }
        public DateTime Fecha { get; set; }

        public List<Producto> Productos { get; set; }

    }
}
