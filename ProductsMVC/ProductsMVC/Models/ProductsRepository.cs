using System;
using System.Data.Entity;

namespace ProductsMVC.Models
{
    public class ProductsRepository: IProductRepository
    {
        private ProductDBContext context;
        
        public ProductsRepository(ProductDBContext db)
        {
            context = db;
        }
   
        public IDbSet<Product> Products { get { return context.Products; } }
        public IDbSet<Category> Categories { get { return context.Categories; } }
        public DbContext Context { get { return context; } }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }
    }
}