using System.ComponentModel.DataAnnotations;
using tl2_tp8_2025_NahuelCondori99.Models;

namespace tl2_tp8_2025_NahuelCondori99
{
    public class PresupuestoViewModel
    {
        public PresupuestoViewModel()
        {
            
        }
        public int IdPresupuesto {get; set;}

        [Required(ErrorMessage = "El nombre o email es obligatorio")]
        [Display(Name = "Nombre o Email del destinatario")]
        public string NombreDestinatario{get; set;}

    }
}