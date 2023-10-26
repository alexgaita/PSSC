using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using static Exemple.Domain.Models.ProductOrder;
using Exemple.Domain.Workflows;
using Exemple.Domain.Commands;

namespace Exemple
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = ReadOrder();
            PlaceOrderCommand command = new(order);
            PlaceOrderWorkflow workflow = new PlaceOrderWorkflow();
            var result = workflow.Execute(command);

            result.Match(
                    whenPlacedOrderScucceededEvent: @event =>
                     {
                        Console.WriteLine($"Order succeeded. {@event.TotalPrice}" );
                        return @event;
                    },
                    whenPlacedOrderFailedEvent: @event =>
                     {
                        Console.WriteLine($"Order Failed failed: {@event.Reason}");
                        return @event;
                    }
                );
        }

        private static UnvalidatedOrder ReadOrder()
        {

            var adress = ReadValue("Adress: ");

            List<UnvalidatedProduct> listOfProducts = new();
            do
            {
                var productCode = ReadValue("Product name: ");
                if (string.IsNullOrEmpty(productCode))
                {
                    break;
                }

                var quantity = ReadValue("Quantity: ");
                if (string.IsNullOrEmpty(quantity))
                {
                    break;
                }

                var price = ReadValue("Price: ");
                if (string.IsNullOrEmpty(price))
                {
                    break;
                }

                listOfProducts.Add(new (productCode, quantity, price));
            } while (true);
            return new UnvalidatedOrder(adress,listOfProducts);
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
