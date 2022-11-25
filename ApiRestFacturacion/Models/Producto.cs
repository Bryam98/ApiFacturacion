using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(10)]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(500)]
        public string  Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioCordoba { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioDolar { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaVencimiento{ get; set; }


        public int IdTasa { get; set; }
        public TasaCambio  TasaCambio { get; set; }

   
    }
}
