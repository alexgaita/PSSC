using System.Collections.Generic;

namespace Exemple.Domain.Models
{
	public record UnvalidatedOrder(string Adress, List<UnvalidatedProduct> Products);
	public record UnvalidatedProduct(string ProductCode, string Quantity, string Price);
}
