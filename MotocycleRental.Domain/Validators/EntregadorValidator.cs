using FluentValidation;
using MotorcycleRental.Domain.Models;


namespace MotorcycleRental.Domain.Validators
{
    public class EntregadorValidator : AbstractValidator<EntregadorRequestModel>
    {
        public EntregadorValidator()
        {


            RuleFor(x => x.Cnpj)
           .NotEmpty().WithMessage("O campo Cnpj é obrigatório.")
           .Must(ValidatorCnpj.IsCnpj).WithMessage("Cnpj inválido.");

            RuleFor(x => x.NumeroCnh)
            .NotEmpty().WithMessage("O campo NumeroCnh precisa ser fornecido")
           .NotNull().WithMessage("O campo NumeroCnh precisa ser fornecido");


            RuleFor(x => x.ImagemDocumento)
            .NotEmpty().WithMessage("O campo ImagemDocumento precisa ser fornecido")
           .NotNull().WithMessage("O campo ImagemDocumento ser fornecido");

            RuleFor(x => x.TipoCnh)
            .Must(l => l.All(valor => valor == "A" || valor == "B"))
            .WithMessage("O campo TipoCnh precisa ser 'A' ou 'B'.");

        }
    }
}