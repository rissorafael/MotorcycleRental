using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Domain.Models
{
    public class AutenticacaoResponseModel : Validator
    {
        public string Token { get; set; }
    }
}
