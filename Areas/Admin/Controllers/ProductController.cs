﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Models;
using ASP_MVC.Dao.IRepository;
using X.PagedList;
using ASP_MVC.ViewModels;
using ASP_MVC.Dao;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Product
        public  IActionResult Index(int? page)
        {
            if (page == null) page = 1;

            int pageSize = 7;


            int pageNumber = (page ?? 1);

            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetEntities(
                filter: null,
                orderBy: null,
                includeProperties: "Category,CoverType"
            );


            return View(productList.ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/Product/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.ProductRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.ProductRepository.GetEntityById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Upsert

        public IActionResult Upsert(int? id)
        {

            ProductDTO productDTO = new ProductDTO();

            productDTO.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            productDTO.CoverTypeList = _unitOfWork.CoverTypeRepository.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            Product product;
            if (id == null || id == 0)
            {
                product = new Product();
            }
            else
            {
                product = _unitOfWork.ProductRepository.GetEntityById((int)id);

                if (product == null)
                {
                    return NotFound();
                }
            }

            productDTO.Product = product;
            return View(productDTO);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(
        [Bind("Id,Name,Author,ISBN,Price,Price50,Price100,Description,CategoryId,CoverTypeId")]
        ProductDTO productDTO,
        IFormFile? file)
        {
            bool IsCreate = productDTO.Product.Id == 0;

            if (ModelState.IsValid)
             {
                
                var wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    if(productDTO.Product.ImageUrl != null && productDTO.Product.ImageUrl != "")
                    {

                        var oldImagePath = Path.Combine(wwwRootPath, productDTO.Product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }


                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, "images/product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {

                        file.CopyTo(fileStream);

                    }

                    productDTO.Product.ImageUrl = fileName;

                }
                if(IsCreate && file == null)
                {
                    TempData["uploadFileError"] = "Require Image";
                }

                if (IsCreate)
                {
         
                    _unitOfWork.ProductRepository.Add(productDTO.Product);

                }
                else
                {

                    _unitOfWork.ProductRepository.Update(productDTO.Product);

                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
             return View(productDTO);
        }



        // GET: Admin/Product/Edit/5
       
        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Admin/Product/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.ProductRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.ProductRepository.GetEntityById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.ProductRepository.GetEntityById == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = _unitOfWork.ProductRepository.GetEntityById(id);

            if (product != null)
            {
               _unitOfWork.ProductRepository.Delete(product);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
