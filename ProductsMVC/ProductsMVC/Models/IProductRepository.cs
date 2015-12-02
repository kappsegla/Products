using System.Collections.Generic;
using System.Data.Entity;

namespace ProductsMVC.Models
{
    public interface IProductRepository
    {
        IDbSet<Product> Products { get; }
        IDbSet<Category> Categories { get; }
        DbContext Context { get; }

        void Add(Product product);
    }
}