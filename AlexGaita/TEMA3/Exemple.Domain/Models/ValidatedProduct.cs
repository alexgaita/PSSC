using System.Collections.Generic;

namespace Exemple.Domain.Models
{
	public record ValidatedProduct(ProductCode productCode, Quantity Quantity, Price price);

	public record ValidatedOrder(string Adress,List<ValidatedProduct> Products);
}
