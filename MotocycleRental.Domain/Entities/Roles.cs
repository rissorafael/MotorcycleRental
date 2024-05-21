namespace MotorcycleRental.Domain.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
    }
}
