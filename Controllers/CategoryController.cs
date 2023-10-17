using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using ASP_MVC.Models;
using X.PagedList;
using ASP_MVC.Dao;

namespace ASP_MVC.Controllers
{
    public class CategoryController : Controller
    {
        // private readonly ICategoryRepository _categoryRepository;


        private readonly IUnitOfWork _unitOfWork;

        //private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Category
        public  IActionResult Index(int? page)
        {

            if (page == null) page = 1;

            int pageSize = 10;


            int pageNumber = (page ?? 1);

            var categoryList = _unitOfWork.CategoryRepository.GetAll().ToPagedList(pageNumber, pageSize);

            categoryList.OrderByDescending(i => i.DisplayOrder);

            return View(categoryList);
        }       

        // GET: Categories/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.CategoryRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetEntityById(id);


            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,DisplayOrder,CreatedDateTime")] Category category)
        {

            /// validate
            bool checkCategoryNameExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.Name == category.Name && i.Id != category.Id, null).Any();
            bool checkDisplayOrderExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id, null).Any();

            if (checkCategoryNameExist)
            {
                ModelState.AddModelError("Name", "The category name already exist");
                TempData["categoryNameError"] = "The category name already exist";
            }

            if (checkDisplayOrderExist)
            {
                ModelState.AddModelError("DisplayOrder", "The display order already exist");
                TempData["categoryDisplayOrderError"] = "The display order already exist";
            }


            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["categorySuccess"] = "Add Category Success!!!";

            }else
            {

                TempData["categoryFaile"] = "Add Category Faile!!";

            }

            return View(category);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.CategoryRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetEntityById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,DisplayOrder,CreatedDateTime")] Category category)
        {
            /// validate
            bool checkCategoryNameExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.Name == category.Name && i.Id != category.Id, null).Any();
            bool checkDisplayOrderExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id, null).Any();

            if (checkCategoryNameExist)
            {
                ModelState.AddModelError("Name", "The category name already exist");
                TempData["categoryNameError"] = "The category name already exist";
            }

            if (checkDisplayOrderExist)
            {
                ModelState.AddModelError("DisplayOrder", "The display order already exist");
                TempData["categoryDisplayOrderError"] = "The display order already exist";
            }



            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["categorySuccess"] = "Update Category Success!!!";
                    _unitOfWork.CategoryRepository.Update(category);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                { 
                    TempData["categoryFaile"] = "Update Category Faile!!!";
                    throw;
                }
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.CategoryRepository == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CategoryRepository.GetEntityById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.CategoryRepository == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Category'  is null.");
            }
            var category =  _unitOfWork.CategoryRepository.GetEntityById(id);
            if (category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
