namespace MotorcycleRental.Domain.Models
{
    public class ContratoRequestModel
    {
        public int IdEntregador { get; set; }
        public int IdPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int DiasEfetivados { get; set; }
        public decimal ValorTotalLocacao { get; set; }
        public int IdLocacao { get; set; }
    }
}
