namespace ProductsMVC.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsMVC.Models.ProductDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ProductsMVC.Models.ProductDBContext";
        }

        protected override void Seed(ProductsMVC.Models.ProductDBContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Products.AddOrUpdate(i => i.Name,
                new Product
                {
                    Name = "Kalsonger",
                    ArticleNumber = 34872,
                    Price = 100,
                    ImageURL = "kalsonger.png",
                    CategoryId = 1
                },
                 new Product
                 {
                     Name = "Strumpor",
                     ArticleNumber = 44861,
                     Price = 50,
                     ImageURL = "strumpor.png",
                     CategoryId = 1
                 }
            );
        }
    }
}
