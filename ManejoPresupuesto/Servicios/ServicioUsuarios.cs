namespace ManejoPresupuesto.Servicios
{
    public interface IserviciosUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServicioUsuarios: IserviciosUsuarios
    {
        public int ObtenerUsuarioId()
        {
            return 1;
        }

    }
}
