using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Banda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [MaxLength(100)]
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Cantidad de musicos")]
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "La cantidad de integrantes tiene que ser de por lo menos 1.")]
        public int cantidadMusicos { get; set; }


        [Display(Name = "Genero musical")]
        [Required]
        public Genero genero { get; set; }

        //agregado

        [Display(Name = "Telefono de contacto")]
        [MaxLength(30)]
        [RegularExpression(@"^\d{2} \d{4}-\d{4}$", ErrorMessage = "El número de teléfono debe tener el formato 11 1111-2222.")]
        public String telefonoContacto { get; set; }

    }
}
