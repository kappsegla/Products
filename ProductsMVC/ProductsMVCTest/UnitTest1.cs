using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProductsMVC.Models;
using ProductsMVC.Controllers;
using System.Web.Mvc;

namespace ProductsMVCTest
{
    [TestClass]
    public class UnitTest1
    {
        private static Mock<IDbSet<T>> CreateMockSet<T>(IQueryable<T> childlessParents) where T : class 
        {
            var mockSet = new Mock<IDbSet<T>>();
        
            mockSet.Setup(m => m.Provider).Returns(childlessParents.Provider);
            mockSet.Setup(m => m.Expression).Returns(childlessParents.Expression);
            mockSet.Setup(m => m.ElementType).Returns(childlessParents.ElementType);
            mockSet.Setup(m => m.GetEnumerator()).Returns(childlessParents.GetEnumerator());
            return mockSet;
        }

        [TestMethod]
        public void ProductSearchResultSuccess()
        {
          
            Category cat1 = new Category { CategoryId = 1, Name = "Clothes" };
            Category cat2 = new Category { CategoryId = 2, Name = "Tools" };

            Product pro1 = new Product { Name = "AAA", Price = 100, Category = cat1, CategoryId = 1 };
            Product pro2 = new Product { Name = "BBB", Price = 200, Category = cat1, CategoryId = 1 };
            Product pro3 = new Product { Name = "CCC", Price = 300, Category = cat2, CategoryId = 2 };

           //cat1.Products.Add(pro1);
           //cat1.Products.Add(pro2);
           //cat2.Products.Add(pro3);
            
            //Create Products
            var data = new List<Product>
            {
                pro1,pro2,pro3
            }.AsQueryable();

            var mockSet = CreateMockSet<Product>(data);
         
            //Create Categories
            var dataCategories = new List<Category>
            {
                cat1, cat2
            }.AsQueryable();

            var mockSetC = CreateMockSet<Category>(dataCategories);

            
            var mockrepo = new Mock<IProductRepository>();
            mockrepo.Setup(m => m.Products).Returns(mockSet.Object);
            mockrepo.Setup(m => m.Categories).Returns(mockSetC.Object);
            
            // The following line bypasses the Include call.
            // var query = mockrepo.Object.Products.Include(p => p.Category).Select(p => p);
            var controller = new ProductsController(mockrepo.Object);
            //var result = controller.List("",1) as ViewResult;
            var result = controller.Index("B");
            var productsvm = (IEnumerable<ProductsMVC.Models.Product>)result.ViewData.Model;
            Assert.AreEqual("BBB", productsvm.ElementAt(0).Name);
        }
        [TestMethod]
        public void TestMethod2() {

            var mock = new Mock<ControllerContext>();
            string userName = "Martin";

            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);


            var mockrepo = new Mock<IProductRepository>();
            //Fill mockrepo with data

            var controller = new ProductsController(mockrepo.Object);
            controller.ControllerContext = mock.Object;
            
        }

        [TestMethod]
        public void TestAddProduct()
        {
            var mockrepo = new Mock<IProductRepository>();
       
            var controller = new ProductsController(mockrepo.Object);
            controller.SinglePage();

            mockrepo.Setup<object>(m => m.GetType()).Returns("");
            mockrepo.Verify(p => p.Add(It.Is<Product>(t => t.Name == "Product1")));

        }
    }
}