using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
