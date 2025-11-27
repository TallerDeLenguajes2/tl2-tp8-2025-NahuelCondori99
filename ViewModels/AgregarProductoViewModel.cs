using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tl2_tp8_2025_NahuelCondori99
{
    public class AgregarProductoViewModel
    {
        public int IdPresupuesto {get; set;}

        [Display(Name = "Producto")]
        public int IdProducto {get; set;}

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
        public int Cantidad {get; set;}

        public SelectList ListaProductos {get; set;}
    }
}