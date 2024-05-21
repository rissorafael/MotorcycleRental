using Microsoft.AspNetCore.Http;

namespace MotorcycleRental.Domain.Models
{
    public class EntregadorRequestModel
    {
        public string UserName { get; set; }
        public string Cnpj { get; set; }
        public long NumeroCnh { get; set; }
        public List<string> TipoCnh { get; set; }
        public IFormFile ImagemDocumento { get; set; }

    }
}
