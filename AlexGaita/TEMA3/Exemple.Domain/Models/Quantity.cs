using Exemple.Domain.Exceptions;
using System;

namespace Exemple.Domain.Models
{
	public record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidQuantityException($"{value} is an invalid quantity value.");
            }
        }


        public static bool TryParseQuantity(string quantityString, out Quantity quantity)
        {
            bool isValid = false;
            quantity = null;
            if(int.TryParse(quantityString, out int numericQuanity))
            {
                if (IsValid(numericQuanity))
                {
                    isValid = true;
                    quantity = new(numericQuanity);
                }
            }

            return isValid;
        }

        private static bool IsValid(decimal numericGrade) => numericGrade >= 1;
    }
}
