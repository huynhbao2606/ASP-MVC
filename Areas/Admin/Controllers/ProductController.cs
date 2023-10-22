using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASP_MVC.Dao.IRepository;
using X.PagedList;
using ASP_MVC.Dao;
using System.Collections;
using Microsoft.AspNetCore.Hosting;

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

        // GET: Admin/Product/Create

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name");
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,ImageUrl,CategoryId,CoverTypeId")] Product product, IFormFile file)
         {
            
            if (ModelState.IsValid)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, "images/product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.ImageUrl = fileName;
                }
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name",product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name",product.CoverTypeId);
            return View(product);
        }



        // GET: Admin/Product/Edit/5
        public IActionResult Edit(int? id)
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
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name", selectedValue: product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name", selectedValue: product.CoverTypeId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,ImageUrl,CategoryId,CoverTypeId")] Product product, IFormFile file)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var productPath = Path.Combine(wwwRootPath, "images/product");
                        var oldImagePath = Path.Combine(productPath, product.ImageUrl);

                        if (System.IO.File.Exists(oldImagePath) && product.ImageUrl != "default.jpg")
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        product.ImageUrl = fileName;
                    }

                    _unitOfWork.ProductRepository.Update(product);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name", product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name", product.CoverTypeId);
            return View(product);
        }

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
