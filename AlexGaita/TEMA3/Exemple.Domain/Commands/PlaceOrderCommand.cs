using System.Collections.Generic;
using Exemple.Domain.Models;

namespace Exemple.Domain.Commands
{
    public record PlaceOrderCommand
    {
        public PlaceOrderCommand(UnvalidatedOrder inputOrder)
        {
            InputOrder = inputOrder;
        }

        public UnvalidatedOrder InputOrder { get; }
    }
}
