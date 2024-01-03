using BrewUpSagas.Messages.Commands;
using BrewUpSagas.Shared.BindingContracts;
using BrewUpSagas.Shared.CustomTypes;
using BrewUpSagas.Shared.DomainIds;

namespace BrewUpSagas.Facade.Converters;

public static class SagasConverter
{
    internal static StartBrewOrderSaga ToCommand(this BrewOrderContract contract)
    {
        return new(new BrewOrderId(Guid.NewGuid()), Guid.NewGuid(), new BrewOrderNumber(contract.BrewOrderNumber),
            new ReceivedOn(contract.ReceivedOn), new BrewOrderBody(contract.BrewOrderBody));
    }
}