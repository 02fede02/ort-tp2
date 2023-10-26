namespace Recital_PNT_MVC.Models
{
    public class Recital
    {
        private int id;
        private DateTime fecha;
        private int entradasVendidas;
        private Boolean estaAgotado;
        private Banda[] bandas;
        private Establecimiento establecimiento;

        public Recital(int id, DateTime fecha, int entradasVendidas, Establecimiento establecimiento)
        {
            this.id = id;
            this.fecha = fecha;
            this.entradasVendidas = entradasVendidas;
            this.estaAgotado = false;
            this.bandas = new Banda[3];
            this.establecimiento = establecimiento;
        }

        private void organizarRecital()
        {

        }

    }
}
