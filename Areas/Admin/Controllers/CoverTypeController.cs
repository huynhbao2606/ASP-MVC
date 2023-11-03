using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using ASP_MVC.Dao.IRepository;
using X.PagedList;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/CoverType
        public  IActionResult Index(int? page)
        {
            if (page == null) page = 1;

            int pageSize = 7;


            int pageNumber = (page ?? 1);

            var categoryList = _unitOfWork.CoverTypeRepository.GetAll().OrderBy(i => i.Name).ToPagedList(pageNumber, pageSize);


            return View(categoryList);
        }

        // GET: Admin/CoverType/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _unitOfWork.CoverTypeRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverTypeRepository.GetEntityById;
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }




        // GET: Admin/Product/Upsert

        public IActionResult Upsert(int? id)
        {

            CoverType coverType;

            if (id == null || id == 0)
            {
                coverType = new CoverType();
            }
            else
            {
                coverType = _unitOfWork.CoverTypeRepository.GetEntityById((int)id);

                if (coverType == null)
                {
                    return NotFound();
                }
            }

          
            return View(coverType);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(
        [Bind("Id,Name")]
        CoverType coverType)
        {
            bool IsCreate = coverType.Id == 0;

            if (ModelState.IsValid)
            {

                if (IsCreate)
                {

                    _unitOfWork.CoverTypeRepository.Add(coverType);

                }
                else
                {

                    _unitOfWork.CoverTypeRepository.Update(coverType);

                }

                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(coverType);
        }

       

        

        // GET: Admin/CoverType/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _unitOfWork.CoverTypeRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverTypeRepository.GetEntityById(id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        // POST: Admin/CoverType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")]CoverType coverType)
        {
            if (id != coverType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CoverTypeRepository.Update(coverType);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        throw;
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        // GET: Admin/CoverType/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _unitOfWork.CoverTypeRepository.GetEntityById == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverTypeRepository.GetEntityById(id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST: Admin/CoverType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_unitOfWork.CoverTypeRepository.GetEntityById == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CoverTypes'  is null.");
            }
            var coverType = _unitOfWork.CoverTypeRepository.GetEntityById(id);
            if (coverType != null)
            {
                _unitOfWork.CoverTypeRepository.Delete(coverType);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

    }
}
