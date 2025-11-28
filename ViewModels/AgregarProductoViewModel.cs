using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace tl2_tp8_2025_NahuelCondori99.ViewModels
{
    public class AgregarProductoViewModel
    {
        public int IdPresupuesto {get; set;}

        [Required]
        public int IdProducto {get; set;}

        [Required]
        [Range(1, 999, ErrorMessage ="Debe ingresar una cantidad valida")]
        public int Cantidad {get; set;}

        public List<Productos> ProductosDisponibles {get ; set;}
    }
}