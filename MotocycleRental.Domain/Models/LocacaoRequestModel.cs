namespace MotorcycleRental.Domain.Models
{
    public class LocacaoRequestModel
    {
        public int MotoId { get; set; }
        public int Entregadorid { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataPrevistoFim { get; set; }
        public int PlanosId { get; set; }
    }
}
