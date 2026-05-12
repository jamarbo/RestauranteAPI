//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class CategoriaData
    {
        private readonly ConexionBD _conexion;
        public CategoriaData(ConexionBD conexion) { _conexion = conexion; }

        public async Task<List<Categoria>> Listar()
        {
            var oLista = new List<Categoria>();
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_listar_categorias", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Categoria
                        {
                            id_categoria = Convert.ToInt32(dr["id_categoria"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }
        public async Task<bool> Registrar(Categoria objeto)
        {
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO Categorias (nombre) VALUES (@nombre)", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.CommandType = CommandType.Text;
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> Editar(Categoria objeto)
        {
            bool respuesta = false;
            using (var con = _conexion.Conectar())
            {
                try
                {
                    await con.OpenAsync();
                    // Usamos una consulta directa o un Store Procedure si prefieres
                    string query = "UPDATE Categorias SET nombre= @nombre WHERE id_categoria = @id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id", objeto.id_categoria);
                    cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                    cmd.CommandType = CommandType.Text;

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    respuesta = filasAfectadas > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en Editar Categoria: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}