using Microsoft.Data.SqlClient;
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
    }
}