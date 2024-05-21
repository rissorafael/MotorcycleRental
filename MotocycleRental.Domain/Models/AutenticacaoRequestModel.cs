using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Domain.Models
{
    public class AutenticacaoRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


    }
}
