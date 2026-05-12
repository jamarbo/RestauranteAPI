using System.Data.SqlClient;
using System.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class PlatoData
    {
        private readonly ConexionBD _conexion;
        public PlatoData(ConexionBD conexion) { _conexion = conexion; }

        public async Task<List<Plato>> Listar()
        {
            var oLista = new List<Plato>();
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_listar_platos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Plato
                        {
                            id_plato = Convert.ToInt32(dr["id_plato"]),
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString(),
                            precio = Convert.ToDecimal(dr["precio"]),
                            id_categoria = Convert.ToInt32(dr["id_categoria"]),
                            activo = Convert.ToBoolean(dr["activo"])
                        });
                    }
                }
            }
            return oLista;
        }
        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = false;
            using (var con = _conexion.Conectar())
            {
                try
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Platos WHERE id_plato = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    // Log error si es necesario
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Registrar(Plato objeto)
        {
            bool respuesta = false;
            using (var con = _conexion.Conectar())
            {
                try
                {
                    await con.OpenAsync();
                    // Consulta ajustada a las columnas de tu imagen
                    string query = @"INSERT INTO Platos (nombre, descripcion, precio, id_categoria) 
                             VALUES (@nombre, @desc, @precio, @idCat)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                    cmd.Parameters.AddWithValue("@desc", objeto.descripcion);
                    cmd.Parameters.AddWithValue("@precio", objeto.precio);
                    cmd.Parameters.AddWithValue("@idCat", objeto.id_categoria);
                    cmd.CommandType = CommandType.Text;

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    // Puedes ver el error en la consola de depuración si falla
                    Console.WriteLine(ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}