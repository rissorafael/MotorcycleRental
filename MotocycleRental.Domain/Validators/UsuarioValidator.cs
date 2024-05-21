using FluentValidation;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequestModel>
    {
        public UsuarioValidator()
        {
           // RuleFor(x => x.Telefone)
           //.NotEmpty()
           //.Length(10).WithMessage("O campo Telefone deve possuior até 13 números.");
        }
    }
}