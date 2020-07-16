using FluentValidation;
using ServerlessMoedas.Models;

namespace ServerlessMoedas.Validators
{
    public class CotacaoMoedaValidator : AbstractValidator<CotacaoMoeda>
    {
        public CotacaoMoedaValidator()
        {
            RuleFor(c => c.Sigla).NotEmpty().WithMessage("Preencha o campo 'Sigla'");

            RuleFor(c => c.Valor).NotEmpty().WithMessage("Preencha o campo 'Valor'")
                .GreaterThan(0).WithMessage("O campo 'Valor' deve ser maior do 0");
        }
    }
}