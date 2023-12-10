using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace MVCBasico.Models
{
    public class Recital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Display(Name = "Nombre")]
        [Required]
        [MaxLength(50)]
        public String? Nombre { get; set; }


        [Display(Name = "Fecha del recital")]
        [Required]
        public DateTime Fecha { get; set; }



        //[Range(0, double.MaxValue, ErrorMessage = "La cantidad de entradas vendidas tiene que ser mayor o igual a 0.")]
        [Display(Name = "Entradas vendidas")]
        public int? EntradasVendidas { get; set; }



        [Display(Name = "Esta agotado")]
        public Boolean EstaAgotado { get; set; }


        [Range(1, double.MaxValue, ErrorMessage = "El precio de la entrada tiene que ser de al menos 1.")]
        [Display(Name = "Precio por cada entrada")]
        [Required]
        public int PrecioEntrada { get; set; }


        [Display(Name = "Banda")]
        [Required]
        public int BandaId { get; set; }
        public Banda? Banda { get; set; }


        [Display(Name = "Establecimiento")]
        [Required]
        public int EstablecimientoId { get; set; }
        public Establecimiento? Establecimiento { get; set; }


        public Recital()
        {
            EntradasVendidas = 0;
            EstaAgotado = false;
        }
    }
}
