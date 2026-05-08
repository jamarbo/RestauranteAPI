namespace RestauranteAPI.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; } // Coincide con tu PK
        public string nombre_completo { get; set; }
        public string nombre_usuario { get; set; }
        public string password_hash { get; set; }
        public string rol { get; set; }
    }
}