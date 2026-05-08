using Microsoft.Data.SqlClient;

namespace RestauranteAPI.Data
{
    public class ConexionBD
    {
        private readonly string _cadenaSQL;

        public ConexionBD(IConfiguration configuration)
        {
            // Lee "DefaultConnection" de tu appsettings.json
            _cadenaSQL = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection Conectar()
        {
            return new SqlConnection(_cadenaSQL);
        }
    }
}