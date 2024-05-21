namespace MotorcycleRental.Domain.Entities
{
    public class Planos
    {
        public int Id { get; set; }
        public decimal ValorDiaria { get; set; }
        public int QuantidadeDias { get; set; }
        public int JurosDiariasNEfetivadas { get; set; }
        public string Descricao { get; set; }
    }
}
