using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class Productora
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(100)]
        [Display(Name = "Razon social")]
        [Required]
        public String RazonSocial { get; set; }


        [Display(Name = "Presupuesto")]
        [Range(1, double.MaxValue, ErrorMessage = "El presupuesto tiene que ser de por lo menos 1.")]
        [Required]
        public int Presupuesto { get; set; }


        [Display(Name = "Recital")]
        [Required]
        public Recital Recitales { get; set; }


        [Display(Name = "Venta entradas")]
        [Required]
        public VentaEntradas VentaEntradas { get; set; }

    }
}
