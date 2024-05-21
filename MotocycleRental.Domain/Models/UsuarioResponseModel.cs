
using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Domain.Models
{
    public class UsuarioResponseModel : Validator
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimnto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
