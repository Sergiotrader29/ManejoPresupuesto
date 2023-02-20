using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IrepositoriosTipoCuentas
    {
        Task Crear(TipoCuentas tipoCuentas);
        Task<bool> Existe(string nombre, int usuarioId);
    }
    public class RepositoriosTiposCuentas: IrepositoriosTipoCuentas
    {
        private readonly string ConnectionString;

        public RepositoriosTiposCuentas(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public  async Task Crear(TipoCuentas tipoCuentas)
        {
            using var connection = new SqlConnection(ConnectionString); // Import Microsoft.Data.SqlClient;
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO TiposCuentas (Nombre, UsuarioId, Orden)
                                                            values (@Nombre, @UsuarioId, 0);
                                                            SELECT SCOPE_IDENTITY();", tipoCuentas);
            /// Hace un query que estoy seguro que solo trae un resultado
            
            tipoCuentas.Id = id;
        }

         public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1
                                  FROM TiposCuentas
                                  WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;",
                                  new { nombre, usuarioId }); //El primer registro o valor defecto
            return existe == 1;
        }

    }
}
