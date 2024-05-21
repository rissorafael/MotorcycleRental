using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Domain.Models
{
    public class EntregadorResponseModel : Validator
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public long NumeroCnh { get; set; }
        public string TipoCnh { get; set; }
        public string ImagemId { get; set; }
    }
}
