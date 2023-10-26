namespace Recital_PNT_MVC.Models
{
    public class Banda
    {
        private int id;
        private string nombre;
        private Boolean esBandaPrincipal;
        Genero genero;

        public Banda(int id, string nombre, Boolean esBandaPrincipal, Genero genero)
        {
            this.id = id;
            this.nombre = nombre;
            this.esBandaPrincipal = esBandaPrincipal;
            this.genero = genero;
        }

        public void modificarBanda()
        {

        }

        public void darAtlaBanda()
        {

        }

    }
}
