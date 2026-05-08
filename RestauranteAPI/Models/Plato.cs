namespace RestauranteAPI.Models
{
    public class Plato
    {
        public int id_plato { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int id_categoria { get; set; } // Relación con Categorías
        public bool activo { get; set; }
    }
}