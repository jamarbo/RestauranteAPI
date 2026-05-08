using Microsoft.Data.SqlClient;
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
    }
}