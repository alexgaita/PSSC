using Exemple.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Exemple.Domain.Models.ProductOrder;

namespace Exemple.Domain.Operations
{
    public static class ProductOrderOperations
    {

        public static IProductOrder ValidateOrder(UnvalidatedOrder order)
    {
        List<ValidatedProduct> validatedProducts = new();
        bool isValidList = true;
        string invalidReson = string.Empty;
        foreach (var unvalidatedProduct in order.Products)
        {
            if (!Quantity.TryParseQuantity(unvalidatedProduct.Quantity, out Quantity quantity))
            {
                invalidReson = $"Invalid quantity ({unvalidatedProduct.ProductCode}, {unvalidatedProduct.Quantity})";
                isValidList = false;
                break;
            }
            if (!Price.TryParsePrice(unvalidatedProduct.Price, out Price price))
            {
                invalidReson = $"Invalid price ({unvalidatedProduct.ProductCode}, {unvalidatedProduct.Price})";
                isValidList = false;
                break;
            }
            if (!ProductCode.TryParse(unvalidatedProduct.ProductCode, out ProductCode productCode))
            {
                invalidReson = $"Invalid product code ({unvalidatedProduct.ProductCode})";
                isValidList = false;
                break;
            }
            ValidatedProduct validGrade = new(productCode, quantity, price);
            validatedProducts.Add(validGrade);
        }

        if (isValidList)
        {
            return new ValidatedProductOrder(new ValidatedOrder(order.Adress, validatedProducts));
        }
        else
        {
            return new InvalidatedProductOrder(invalidReson);
        }

    }

    public static IProductOrder VerifyStock(IProductOrder order) => order.Match(
            whenInvalidatedProductOrder: invalidOrder => invalidOrder,
            whenValidatedProductOrder: validOrder => {
                List<ValidatedProduct> validatedProducts = new();
                bool isValidList = true;
                string invalidReson = string.Empty;
                foreach (var validatedProduct in validOrder.ValidatedOrder.Products)
                {
                    if (validatedProduct.Quantity.Value > 100)
                    {
                        invalidReson = $"Not enough stock ({validatedProduct.productCode}, {validatedProduct.Quantity})";
                        isValidList = false;
                        break;
                    }
                    validatedProducts.Add(validatedProduct);
                }

                if (isValidList)
                {
                    return new ValidatedStock(validOrder.ValidatedOrder);
                }
                else
                {
                    return new InvalidatedProductOrder(invalidReson);
                }

            },
            whenValidatedStock: validStock => validStock,
            whenCalculatedPrice: calculatedPrice => calculatedPrice
        );


        public static IProductOrder CalculateTotalPrice(IProductOrder order) => order.Match(
            whenInvalidatedProductOrder: invalidOrder => invalidOrder,
            whenValidatedProductOrder: validOrder => validOrder,
            whenValidatedStock: validStock => {

                decimal totalPrice = 0;

                foreach (var validatedProduct in validStock.ValidatedOrder.Products)
                {
                    totalPrice += validatedProduct.price.Value * validatedProduct.Quantity.Value;
                }

                return new CalculatedPrice(validStock.ValidatedOrder,new Price(totalPrice));
            },
            whenCalculatedPrice: calculatedPrice => calculatedPrice
        );

    
           
    }
}
