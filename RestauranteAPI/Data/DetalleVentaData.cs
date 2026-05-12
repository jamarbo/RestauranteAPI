using System.Data.SqlClient;
using System.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Data
{
    public class DetalleVentaData
    {
        private readonly ConexionBD _conexion;
        public DetalleVentaData(ConexionBD conexion) { _conexion = conexion; }

        public async Task<bool> RegistrarDetalle(DetalleVenta objeto)
        {
            using (var con = _conexion.Conectar())
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO DetalleVenta (id_venta, id_plato, cantidad, precio_unitario) VALUES (@id_venta, @id_plato, @cantidad, @precio_unitario)", con);
                cmd.Parameters.AddWithValue("@id_venta", objeto.id_venta);
                cmd.Parameters.AddWithValue("@id_plato", objeto.id_plato);
                cmd.Parameters.AddWithValue("@cantidad", objeto.cantidad);
                cmd.Parameters.AddWithValue("@precio_unitario", objeto.precio_unitario);
                cmd.CommandType = CommandType.Text;

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}