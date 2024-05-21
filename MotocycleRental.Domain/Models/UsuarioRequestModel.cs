
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Domain.Models
{
    public class UsuarioRequestModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string UserName { get; set; }
    
        [Required]
        public DateTime DataNascimento { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
       
        [Required]
        [Compare("Senha")]
        public string ReSenha { get; set; }

        [Required]
        public string Email { get; set; }
      
        [Required]
        public string Telefone { get; set; }
    }
}
