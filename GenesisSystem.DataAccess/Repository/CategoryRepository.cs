using GenesisSystem.DataAccess.Repository.IRepository;
using GenesisSystem.Models;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GenesisSystem.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        private ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public CategoryRepository(ApplicationDbContext db, IConfiguration configuration) :base(db)
        {
            _db = db;
            _configuration = configuration;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

        public async Task<bool> PostAsync(Category obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PostAsJsonAsync("CategoryAPI/Create", obj);
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

        public async Task<Category> GetAsync(int? id)
        {
            Category Category = null;
            if (id == null)
            {
                return Category;
            }

            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"CategoryAPI/GetCategory/{id}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    Category = JSserializer.Deserialize<Category>(data);
                }
                
            }

            if (Category == null)
            {
                return Category;
            }
            return Category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            IEnumerable<Category> Categories = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var result = await client.GetAsync("CategoryAPI/GetAll");

                    if (result.IsSuccessStatusCode)
                    {
                        var data = await result.Content.ReadAsStringAsync();
                        JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                        //deserialize to your class
                        Categories = JSserializer.Deserialize<List<Category>>(data);
                        return Categories;
                    }
                    else
                    {
                        Categories = Enumerable.Empty<Category>();
                        return Categories;
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

                    var response = await client.DeleteAsync($"CategoryAPI/Delete/{id}");
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

        public async Task<bool> UpdateAsync(Category obj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                    var response = await client.PutAsJsonAsync("CategoryAPI/Update", obj);
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

        public async Task<IEnumerable<Product>> ProductListAsync(int? CategoryId)
        {
            IEnumerable<Product> CategoryProduct = null;
            if (CategoryId == null)
            {
                return CategoryProduct;
            }


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("API:Address").Value);

                var result = await client.GetAsync($"CategoryAPI/GetProductList/{CategoryId}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                    //deserialize to your class
                    CategoryProduct = JSserializer.Deserialize<IEnumerable<Product>>(data);
                }

            }

            if (CategoryProduct == null)
            {
                return CategoryProduct;
            }
            return CategoryProduct;
        }
    }
}
