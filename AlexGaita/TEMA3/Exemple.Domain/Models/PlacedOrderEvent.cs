using CSharp.Choices;
using System;

namespace Exemple.Domain.Models
{
	[AsChoice]
    public static partial class PlacedOrderEvent
    {
        public interface IPlacedOrderEvent { }

        public record PlacedOrderScucceededEvent : IPlacedOrderEvent {
            public ValidatedOrder Order { get; }
            public Price TotalPrice { get; }

            internal PlacedOrderScucceededEvent(ValidatedOrder order,Price totalPrice)
            {
                Order = order;
                TotalPrice = totalPrice;
            }

        }

        public record PlacedOrderFailedEvent : IPlacedOrderEvent 
        {
            public string Reason { get; }

            internal PlacedOrderFailedEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
