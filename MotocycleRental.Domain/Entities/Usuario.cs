﻿
namespace MotorcycleRental.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

    }
}
