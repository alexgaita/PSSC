using CSharp.Choices;
using System;
using System.Collections.Generic;

namespace Exemple.Domain.Models
{
	[AsChoice]
    public static partial class ProductOrder
    {
        public interface IProductOrder { }

        public record InvalidatedProductOrder: IProductOrder
        {
            internal InvalidatedProductOrder(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }

        public record ValidatedProductOrder: IProductOrder
        {
            internal ValidatedProductOrder(ValidatedOrder validatedOrder)
            {
                ValidatedOrder = validatedOrder;
            }

            public ValidatedOrder ValidatedOrder { get; }
        }

        public record ValidatedStock : IProductOrder
        {
            internal ValidatedStock(ValidatedOrder validatedOrder)
            {
                ValidatedOrder = validatedOrder;
            }

            public ValidatedOrder ValidatedOrder { get; }
        }
       
        public record CalculatedPrice : IProductOrder
        {
            internal CalculatedPrice(ValidatedOrder validatedOrder,Price totalPrice)
            {
                ValidatedOrder = validatedOrder;
                TotalPrice = totalPrice;
            }

            public ValidatedOrder ValidatedOrder { get; }
            public Price TotalPrice { get; }
        }
       
    }
}
