using FluentValidation;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Validators
{
    public class MotoValidator : AbstractValidator<MotoRequestModel>
    {
        public MotoValidator()
        {
            RuleFor(x => x.Ano)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(x => x.Placa)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");


        }
    }
}
