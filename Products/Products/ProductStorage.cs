using System;
using System.Collections.Generic;
using System.Linq;

namespace Products
{
    public class ProductStorage
    {
        private List<Product> productdb = new List<Product>();

        public ProductStorage()
        {
        }

        public void AddNewProduct(Product p) {
            productdb.Add(p);
        }

        public int ProductCount() {
            return productdb.Count;
        }

        public List<Product> ProductsSortedByName()
        {
            var subset = from product in productdb
                         orderby product.ProductName
                         select product;
            
            return subset.ToList<Product>();
        }


        public void RemoveProductID(int productid)
        {
            foreach (var product in productdb)
            {
                if(product.ProductID == productid)
                {
                    productdb.Remove(product);
                    break;
                }
            }
        }

        public List<Product> ProductsSortedByPrice()
        {
            var subset = from product in productdb
                         orderby product.Price
                         select product;

            return subset.ToList<Product>();
        }
    }
}