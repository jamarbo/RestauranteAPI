using System.Data;
using System.Data.SqlClient;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class ClienteData
    {
        private readonly ConexionBD _conexion;

        public ClienteData(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<Cliente>> Listar()
        {
            var oLista = new List<Cliente>();
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT id_cliente, nombre, telefono, email, direccion_defecto FROM Clientes", con);
                cmd.CommandType = CommandType.Text;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Cliente
                        {
                            id_cliente = Convert.ToInt32(dr["id_cliente"]),
                            nombre = dr["nombre"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            email = dr["email"].ToString(),
                            direccion_defecto = dr["direccion_defecto"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public async Task<bool> Registrar(Cliente objeto)
        {
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                string query = "INSERT INTO Clientes (nombre, telefono, email, direccion_defecto) VALUES (@nombre, @tel, @mail, @dir)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.Parameters.AddWithValue("@tel", objeto.telefono);
                cmd.Parameters.AddWithValue("@mail", objeto.email);
                cmd.Parameters.AddWithValue("@dir", objeto.direccion_defecto);
                cmd.CommandType = CommandType.Text;

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}