
namespace MotorcycleRental.Domain.Entities
{
    public class Contrato
    {
        public int Id { get; set; }
        public int IdEntregador { get; set; }
        public int IdPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int DiasEfetivados { get; set; }
        public decimal ValorTotalLocacao { get; set; }
        public int IdLocacao { get; set; }
    }
}
