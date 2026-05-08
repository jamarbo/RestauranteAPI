using Microsoft.Data.SqlClient;
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
                // Asegúrate de tener este SP o usa un SELECT simple por ahora
                SqlCommand cmd = new SqlCommand("SELECT id_venta, id_usuario, total, metodo_pago, fecha_venta FROM Ventas", con);
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