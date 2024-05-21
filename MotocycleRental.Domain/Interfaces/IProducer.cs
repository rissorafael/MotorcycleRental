
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IProducer
    {
        void PublicaMenssagem(TransacaoMensagem menssagem);
    }
}
