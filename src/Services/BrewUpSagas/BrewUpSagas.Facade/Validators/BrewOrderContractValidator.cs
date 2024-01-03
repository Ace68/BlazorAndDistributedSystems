using BrewUpSagas.Shared.BindingContracts;
using FluentValidation;

namespace BrewUpSagas.Facade.Validators;

public class BrewOrderContractValidator : AbstractValidator<BrewOrderContract>
{
    public BrewOrderContractValidator()
    {
        RuleFor(v => v.BrewOrderNumber).NotEmpty();
        RuleFor(v => v.BrewOrderBody).NotEmpty();
        RuleFor(v => v.ReceivedOn).GreaterThan(DateTime.MinValue);
    }
}