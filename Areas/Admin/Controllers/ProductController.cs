using System;
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
        [Bind("Product")]ProductDTO productDto,
        IFormFile? file)
        {

            bool isCreate = productDto.Product.Id == 0;

            /// validate
            bool checkProductTitleExist = _unitOfWork.ProductRepository
                .GetEntities(
                    filter: isCreate
                        ? i => i.Title == productDto.Product.Title
                        : i => i.Title == productDto.Product.Title && i.Id != productDto.Product.Id,
                    orderBy: null,
                    includeProperties: null)
                .Any();

            if (checkProductTitleExist)
            {
                ModelState.AddModelError("Name", "The product title already exist");
                TempData["productTitleError"] = "The product title already exist";
            }

            productDto.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

            productDto.CoverTypeList = _unitOfWork.CoverTypeRepository.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            /// save data
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    if (productDto.Product.ImageUrl != null && productDto.Product.ImageUrl != "")
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,
                            productDto.Product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    /// upload to wwwroot/images/products
                    var uploads = Path.Combine(wwwRootPath, "images" + Path.DirectorySeparatorChar + "product");
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }

                    productDto.Product.ImageUrl = Path.DirectorySeparatorChar + "images"
                                                + Path.DirectorySeparatorChar + "product"
                                                + Path.DirectorySeparatorChar + fileName + extension;
                }

                

                if (isCreate && file == null)
                {
                    TempData["uploadFileError"] = "require file";
                    return View(productDto);
                }

                if (isCreate)
                {
                    _unitOfWork.ProductRepository.Add(productDto.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productDto.Product);
                }



                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(productDto);
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (product.ImageUrl != null && product.ImageUrl != "")
                {
                    var oldImagePath = Path.Combine(wwwRootPath,
                        product.ImageUrl.TrimStart(Path.DirectorySeparatorChar));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.ProductRepository.Delete(product);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
