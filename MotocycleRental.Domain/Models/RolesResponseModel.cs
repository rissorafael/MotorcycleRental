
namespace MotorcycleRental.Domain.Models
{
    public class RolesResponseModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
    }
}
