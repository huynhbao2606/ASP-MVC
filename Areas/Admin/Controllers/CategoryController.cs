using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using X.PagedList;
using ASP_MVC.Dao.IRepository;
using ASP_MVC.Models;

namespace ASP_MVC.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {


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

            int pageSize = 7;


            int pageNumber = (page ?? 1);

            var categoryList = _unitOfWork.CategoryRepository.GetAll().OrderBy(i => i.DisplayOrder).ToPagedList(pageNumber,pageSize);


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
        public IActionResult Create([Bind("Id,Name,DisplayOrder,CreatedDateTime")]Category category)
        {
            
            /// validate
            bool checkCategoryNameExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.Name == category.Name && i.Id != category.Id, null,null).Any();
            bool checkDisplayOrderExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id, null,null).Any();

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
                TempData["categorySuccess"] = "Add Category Success!!!";
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
                 return RedirectToAction("Index");

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
        public IActionResult Edit(int id, [Bind("Id,Name,DisplayOrder,CreatedDateTime")]Category category)
        {
            /// validate
            bool checkCategoryNameExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.Name == category.Name && i.Id != category.Id, null,null).Any();
            bool checkDisplayOrderExist = _unitOfWork.CategoryRepository
                .GetEntities(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id, null,null).Any();

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
