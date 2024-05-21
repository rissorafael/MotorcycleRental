using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Domain.Models
{
    public class LocacaoResponseModel : Validator
    {
        public int Id { get; set; }
        public int MotoId { get; set; }
        public int Entregadorid { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevistoFim { get; set; }
        public int PlanosId { get; set; }
        public string Status { get; set; }
        public int DiasPrevisto { get; set; }
        public decimal ValorLocacao { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}
