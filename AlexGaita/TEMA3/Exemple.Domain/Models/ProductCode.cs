using Exemple.Domain.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Exemple.Domain.Models
{
	public record ProductCode
    {
        public string Value { get; }

        private static List<string> availableProductsList = new List<string>() { "caciula", "televizor", "masina de spalat" };

        private ProductCode(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductCodeException("There is no such product in our database.");
            }
        }

        
        private static bool IsValid(string stringValue) => availableProductsList.Contains(stringValue.ToLower());

        public override string ToString()
        {
            return Value;
        }

        public static bool TryParse(string stringValue, out ProductCode productCode)
        {
            bool isValid = false;
            productCode = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                productCode = new(stringValue);
            }

            return isValid;
        }
    }
}
