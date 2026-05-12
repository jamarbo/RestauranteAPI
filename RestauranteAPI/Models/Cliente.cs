namespace RestauranteAPI.Models
{
    public class Cliente
    {
        public int id_cliente { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string direccion_defecto { get; set; }
    }
}