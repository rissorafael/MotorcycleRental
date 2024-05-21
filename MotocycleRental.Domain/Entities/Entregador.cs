
namespace MotorcycleRental.Domain.Entities
{
    public class Entregador
    {
        public int Id { get; set; }
        public string Cnpj { get; set; }
        public long NumeroCnh { get; set; }
        public string TipoCnh { get; set; }
        public string ImagemId { get; set; }
        public int UsuarioId { get; set; }

    }
}
