using System;
using ASP_MVC.Dao;
using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC.Dao
{
    public class CategoryRepository : ICategoryRepository 
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        void ICategoryRepository.Add(Category category)
        {
            _db.Add(category);
        }

        IEnumerable<Category> ICategoryRepository.GetCategories()
        {
            return _db.Categories.ToList();
        }

        Category ICategoryRepository.GetCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }

        void ICategoryRepository.Remove(Category category)
        {
            _db.Remove(category);
        }

        void ICategoryRepository.Save()
        {
            _db.SaveChanges();
        }

        void ICategoryRepository.Update(Category category)
        {
            //_db.Update(category);
            _db.Categories.Attach(category);
            _db.Entry(category).State = EntityState.Modified;
        }
    }
}