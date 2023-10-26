namespace Recital_PNT_MVC.Models
{
    public class Establecimiento
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int capacidad;
        public TipoEstablecimiento tipo;

        public Establecimiento() { }



    }
}
