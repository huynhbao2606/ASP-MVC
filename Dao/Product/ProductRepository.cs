using System;
using ASP_MVC.Dao;
using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC.Dao
{
    public class ProductRepository : IProductRepository 
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        void IProductRepository.Add(Product product)
        {
            _db.Add(product);
        }

        IEnumerable<Product> IProductRepository.GetProducts()
        {
            return _db.Products.ToList();
        }

        Product IProductRepository.GetProductById(int id)
        {
            return _db.Products.Find(id);
        }

        void IProductRepository.Remove(Product product)
        {
            _db.Remove(product);
        }

        void IProductRepository.Save()
        {
            _db.SaveChanges();
        }

        void IProductRepository.Update(Product product)
        {
            //_db.Update(category);
            _db.Products.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
        }
    }
}