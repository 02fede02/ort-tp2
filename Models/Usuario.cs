using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50)]
        [Required]
        public String Nombre { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50)]
        [Required]
        public String Apellido { get; set; }


        [Display(Name = "Mail de Contacto")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email no valido.")]
        [MaxLength(70)]
        public String Email { get; set; }

    }
}
