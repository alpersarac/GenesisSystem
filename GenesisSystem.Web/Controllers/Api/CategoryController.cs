using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenesisSystem.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategories = _unitOfWork.Category.GetAll();
            return View(objCategories);
        }

    }
}
