using System;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
	public interface ICategoryRepository 
    {
		Category GetCategoryById(int id);
        
		IEnumerable<Category> GetCategories();

		void Add(Category category);

        void Update(Category category);

        void Remove(Category category);

		void Save();
    }
}
