using System.Data.SqlClient;
using System.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class VentaData
    {
        private readonly ConexionBD _conexion;
        public VentaData(ConexionBD conexion) { _conexion = conexion; }

        public async Task<int> FinalizarVenta(Venta objeto)
        {
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_FinalizarVenta", con);
                cmd.Parameters.AddWithValue("@id_usuario", objeto.id_usuario);
                cmd.Parameters.AddWithValue("@total", objeto.total);
                cmd.Parameters.AddWithValue("@metodo_pago", objeto.metodo_pago);
                cmd.CommandType = CommandType.StoredProcedure;

                var idGenerado = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(idGenerado);
            }
        }
        public async Task<List<Venta>> Listar()
        {
            var oLista = new List<Venta>();
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();

                // Usamos ALIAS (AS) para que los nombres de SQL coincidan con tu objeto C#
                string query = @"SELECT id_venta, 
                                id_usuario, 
                                total, 
                                estado_pago AS metodo_pago, 
                                fecha_hora AS fecha_venta 
                         FROM Ventas";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        oLista.Add(new Venta
                        {
                            id_venta = Convert.ToInt32(dr["id_venta"]),
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            total = Convert.ToDecimal(dr["total"]),
                            // Ahora estos nombres sí existen en el resultado del SELECT
                            metodo_pago = dr["metodo_pago"].ToString(),
                            fecha_venta = Convert.ToDateTime(dr["fecha_venta"])
                        });
                    }
                }
            }
            return oLista;
        }
    }
}