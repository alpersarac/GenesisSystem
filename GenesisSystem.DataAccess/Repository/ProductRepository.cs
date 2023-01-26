using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public ProductRepository(ApplicationDbContext db, IConfiguration configuration) :base(db)
        {
            _db = db;
            _configuration = configuration;
        }

        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }

        public IEnumerable<Product> GetProductListById(int? id)
        {
            return _db.Products.Where(p => p.Category.Id == id).ToList();
        }
        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public async Task<bool> PostAsync(Product obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PostAsJsonAsync("ProductAPI/Create", obj);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Product> GetAsync(int? id)
        {
            Product product = null;
            if (id == null)
            {
                return product;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"ProductAPI/GetProduct/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    product = JSserializer.Deserialize<Product>(data);
                }

            }

            if (product == null)
            {
                return product;
            }
            return product;
        }

        public async Task<bool> UpdateAsync(Product obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PutAsJsonAsync("ProductAPI/Update", obj);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> Products = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var result = await client.GetAsync("ProductAPI/GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                        //deserialize to your class
                        Products = JSserializer.Deserialize<List<Product>>(data);
                        return Products;
                    }
                    else
                    {
                        Products = Enumerable.Empty<Product>();
                        return Products;
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.DeleteAsync($"ProductAPI/Delete/{id}");
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<IEnumerable<Product>> ProductListAsync(int? CategoryId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdateProductDynamically(int? productId, int categoryId, string? productName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var Items = new { productId = productId, categoryId = categoryId, productName= productName };
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);
                    
                    //var response = await client.PostAsJsonAsync($"ProductAPI/UpdateProductDynamically/{productId}/{categoryId}/{productName}");
                    var response = await client.PostAsJsonAsync($"ProductAPI/UpdateProductDynamically/{productId}/{categoryId}/{productName}", Items);
                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
