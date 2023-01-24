using GenesisSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category obj);
        List<Product> GetProducts(int? id);
    }
}
