using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class VentaEntradas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Cantidad Entradas")]
        [Range(1, double.MaxValue, ErrorMessage = "Minimo debes de comprar una entrada")]
        [Required]
        public int CantidadEntradas { get; set; }

        [Display(Name = "Recital")]
        [Required]
        public int RecitalId { get; set; }
        public Recital? Recital { get; set; }


        [Display(Name = "Usuario")]
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }


        [Display(Name = "Precio total")]
        public int? PrecioTotal {  get; set; }

    }
}
