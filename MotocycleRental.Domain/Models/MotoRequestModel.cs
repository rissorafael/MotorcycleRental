
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Domain.Models
{
    public class MotoRequestModel
    {

        [Required]
        public string Modelo { get; set; }
        public string Marca { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public string Placa { get; set; }
        public int Cilindradas { get; set; }
    }
}
