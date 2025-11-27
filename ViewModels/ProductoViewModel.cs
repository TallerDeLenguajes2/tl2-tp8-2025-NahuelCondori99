using System.ComponentModel.DataAnnotations;

namespace tl2_tp8_2025_NahuelCondori99.ViewModels
{
    public class ProductoViewModel
    {
        public int IdProducto {get; set;}

        [Display(Name = "Descripcion del producto")]
        [StringLength(250, ErrorMessage = "La descripcion no puede superar los 250 caracteres")]
        public string Descripcion{get; set;}

        [Display(Name = "Precio unitario")]
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public int Precio {get; set;}
    }
}