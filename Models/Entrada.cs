using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Entrada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Precio")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio tiene que ser mayor o igual que uno")]
        [Required]
        public int Precio { get; set; }


        [Display(Name = "Cantidad")]
        [Range(1, double.MaxValue, ErrorMessage = "La cantidad tiene que ser de al menos 1 entrada.")]
        [Required]
        public int Cantidad { get; set; }


        [Display(Name = "Establecimiento")]
        [Required]
        public int EstablecimientoId { get; set; }
        public Establecimiento? Establecimiento { get; set; }


        [Display(Name = "Recital")]
        [Required]
        public int RecitalId { get; set; }
        public Recital? Recital { get; set; }


        [Display(Name = "Usuario")]
        [Required]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
