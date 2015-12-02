using ProductsMVC.Filters;
using ProductsMVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace ProductsMVC.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiController
    {
        IProductRepository repository;

        public ProductsController(IProductRepository pr)
        {
            repository = pr;
        }
     
        [HttpGet]
        public IEnumerable<ProductInfo> GetAllProducts()
        {
            var result = repository.Products
                .Select(p => new ProductInfo { Name = p.Name,
                                               Price = p.Price,
                                               Articlenumber = p.ArticleNumber })
                .ToList();
            return result;
        }
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = repository.Products.Where(p => p.ArticleNumber == id)
                          .Select(p => new ProductInfo { Name = p.Name, Price = p.Price, Articlenumber = p.ArticleNumber })
                          .DefaultIfEmpty()
                          .First();

            if (product == null)
            {
                return NotFound();
            } 
            return Ok(product);
        }
        [HttpPost]
        public void CreateProduct([FromBody] Product item)
        {
            repository.Products.Add(item);
            repository.Context.SaveChanges();
        }
        [HttpPut]
        public void UpdateProduct(int id, [FromBody] Product item)
        {
            repository.Context.Entry(item).State = EntityState.Modified;
            repository.Context.SaveChanges();
        }
        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
                var item = repository.Products.FirstOrDefault(x => x.ArticleNumber == id);
                if (item == null)
                {
                    return NotFound();
                }
                repository.Products.Remove(item);
                repository.Context.SaveChanges();
            return Ok();
        }
    }
    public class ProductInfo
    {
        public string Name;
        public decimal Price;
        public long Articlenumber;
    }
}