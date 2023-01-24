using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
        public List<Product> GetProducts(int? id)
        {
            return _db.Products.Where(p => p.CategoryId == id).ToList();
        }
    }
}
