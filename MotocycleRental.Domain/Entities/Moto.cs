
namespace MotorcycleRental.Domain.Entities
{
    public class Moto
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public int Cilindradas { get; set; }
    }
}
