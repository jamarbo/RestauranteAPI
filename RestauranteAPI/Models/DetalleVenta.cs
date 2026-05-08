
namespace RestauranteAPI.Models
{
    public class DetalleVenta
    {
        public int id_detalle { get; set; }
        public int id_venta { get; set; }
        public int id_plato { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public decimal subtotal { get; set; }
    }
}