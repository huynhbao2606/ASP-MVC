using System;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
	public interface IProductRepository 
    {
		Product GetProductById(int id);
        
		IEnumerable<Product> GetProducts();

		void Add(Product product);

        void Update(Product product);

        void Remove(Product product);

		void Save();
    }
}
