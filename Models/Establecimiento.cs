using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Establecimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [MaxLength(50)]
        public String nombre { get; set; }

        [Display(Name = "Capacidad")]
        [Required]
        [Range(50, double.MaxValue, ErrorMessage = "La capacidad tiene que ser de al menos 50 personas.")]
        public int capacidad { get; set; }

        [Display(Name = "Tipo de establecimiento")]
        [Required]
        public Tipo tipo { get; set; }

        //agregado

        [Display(Name = "Mail de contacto")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email no valido.")]
        [MaxLength(70)]
        public String mailContacto { get; set; }
    }
}
