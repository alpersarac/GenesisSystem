using GenesisSystem.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            Category = new CategoryRepository(db, configuration);
            Product = new ProductRepository(db, _configuration);
        }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
