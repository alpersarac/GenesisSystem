
using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenesisSystem.Controllers
{
  
    public class ProductController : Controller
    {
        IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objCoverType = _unitOfWork.Product.GetAll();
            return View(objCoverType);
        }
        //GET
        public IActionResult Create()
        {
            ViewBag.StatesList = _unitOfWork.Category.GetAll();
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            //if (obj.Name == obj.Name.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            //    //Custom Error.
            //    //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has been created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CoverTypeFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            //if (obj.Name == obj.Name.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            //    //Custom Error.
            //    //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the name");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has been updated successfully";

                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CoverTypeFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(CoverTypeFromDb);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category has been deleted successfully";

            return RedirectToAction("Index");

        }
    }
}
