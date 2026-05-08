using System.Data;
using Microsoft.Data.SqlClient;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class UsuarioData
    {
        private readonly ConexionBD _conexion;

        // El constructor recibe la conexión para poder usarla
        public UsuarioData(ConexionBD conexion)
        {
            _conexion = conexion;
        }

        // 1. MÉTODO PARA LISTAR TODOS LOS USUARIOS (GET)
        public async Task<List<Usuario>> Listar()
        {
            var oLista = new List<Usuario>();

            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                // Llamamos al procedimiento almacenado que creamos en SQL
                SqlCommand cmd = new SqlCommand("usp_listar", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Usuario()
                        {
                            // Mapeamos cada columna de tu tabla SQL a la clase C#
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            nombre_completo = dr["nombre_completo"].ToString(),
                            nombre_usuario = dr["nombre_usuario"].ToString(),
                            password_hash = dr["password_hash"].ToString(),
                            rol = dr["rol"] != DBNull.Value ? dr["rol"].ToString() : null
                        });
                    }
                }
            }
            return oLista;
        }

        // 2. MÉTODO PARA OBTENER UN USUARIO ESPECÍFICO (GET por ID)
        public async Task<Usuario> Obtener(int id)
        {
            var oUsuario = new Usuario();

            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("usp_obtener", con);
                cmd.Parameters.AddWithValue("@id_usuario", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        oUsuario.id_usuario = Convert.ToInt32(dr["id_usuario"]);
                        oUsuario.nombre_completo = dr["nombre_completo"].ToString();
                        oUsuario.nombre_usuario = dr["nombre_usuario"].ToString();
                        oUsuario.password_hash = dr["password_hash"].ToString();
                        oUsuario.rol = dr["rol"] != DBNull.Value ? dr["rol"].ToString() : null;
                    }
                }
            }
            return oUsuario;
        }
    }
}