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

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Product
        public  IActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 7;


            int pageNumber = (page ?? 1);

            var productList = _unitOfWork.ProductRepository.GetAll().OrderBy(i => i.Id).ToPagedList(pageNumber, pageSize);


            return View(productList);
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
        public IActionResult Create([Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,ImageUrl,CategoryId,CoverTypeId")] Product product)
         {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name", product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name", product.CoverTypeId);
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
            ViewData["CategoryId"] = new SelectList(_unitOfWork.CategoryRepository.GetAll(), "Id", "Name", product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.CoverTypeRepository.GetAll(), "Id", "Name", product.CoverTypeId);
            return View("create",product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,ImageUrl,CategoryId,CoverTypeId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
