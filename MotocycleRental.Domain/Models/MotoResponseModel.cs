
using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Domain.Models
{
    public class MotoResponseModel : Validator
    {
        public int id { get; set; }
        public string modelo { get; set; }
        public string marca { get; set; }
        public int ano { get; set; }
        public string placa { get; set; }
        public int cilindradas { get; set; }

    }
}
