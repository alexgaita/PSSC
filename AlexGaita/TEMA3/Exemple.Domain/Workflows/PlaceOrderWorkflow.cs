using Exemple.Domain.Models;
using static Exemple.Domain.Models.PlacedOrderEvent;
using static Exemple.Domain.Operations.ProductOrderOperations;

using System;
using static Exemple.Domain.Models.ProductOrder;
using Exemple.Domain.Commands;

namespace Exemple.Domain.Workflows
{
    public class PlaceOrderWorkflow
    {
        public IPlacedOrderEvent Execute(PlaceOrderCommand command)
        {
            UnvalidatedOrder unvalidatedOrder = command.InputOrder;

            IProductOrder order = ValidateOrder(unvalidatedOrder);
            order = VerifyStock(order);
            order = CalculateTotalPrice(order);

            IPlacedOrderEvent result = null;

            order.Match(
                whenInvalidatedProductOrder: invalidOrder =>
                {
                    result = new PlacedOrderFailedEvent(invalidOrder.Reason);
                    return invalidOrder;
                },
                whenValidatedProductOrder: validOrder => {
                    result = new PlacedOrderFailedEvent("Something went wrong.");
                    return validOrder;
                },
                whenValidatedStock: validStock => {
                    result = new PlacedOrderFailedEvent("Something went wrong.");
                    return validStock;
                },
                whenCalculatedPrice: calculatedPrice => {
                    result = new PlacedOrderScucceededEvent(calculatedPrice.ValidatedOrder,calculatedPrice.TotalPrice);
                    return calculatedPrice;
                });

            return result;
        }
    }
}
