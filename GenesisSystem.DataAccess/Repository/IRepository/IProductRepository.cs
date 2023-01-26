using GenesisSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>, IApiCall<Product>,IApiCallForProduct
    {
        void Update(Product obj);
        IEnumerable<Product> GetProductListById(int? CategoryId);
    }
}
