using MSCore.Models;
using System.Collections.Generic;

namespace MSCore.Repository
{
	public interface IProductRepository
	{
		void DeleteProduct(int productId);

		Product GetProductByID(int productId);

		IEnumerable<Product> GetProducts();

		void InsertProduct(Product product);

		void Save();

		void UpdateProduct(Product product);
	}
}
