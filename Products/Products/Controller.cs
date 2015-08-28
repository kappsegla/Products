using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products
{
    public class Controller
    {
        private ProductStorage storage;

        public Controller(ProductStorage pstorage)
        {
            storage = pstorage;
        }

        public void ShowMenu()
        {
            Console.WriteLine("THis is the MENU, 1,2,3");
            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.D1)
                NewProduct();
            else if (key == ConsoleKey.D2)
                RemoveProduct();
            else if (key == ConsoleKey.D3)
                ListProductsByName();
            else if (key == ConsoleKey.D4)
                ListProductsByPrice();
        }

        public void NewProduct()
        {
            //Add a product
            Console.WriteLine("Time to create an object. Please enter the following information.");
            Console.Write("ProductName:");
            string name = Console.ReadLine();
            Console.Write("ProductID:");
            int id = Int32.Parse(Console.ReadLine());
            Console.Write("Price:");
            int price = Int32.Parse(Console.ReadLine());
            //...
            Product product = new Product();
            product.ProductName = name;
            product.ProductID = id;
            product.ProductDescription = "Generic product description.";
            product.Price = price;
            //...
            storage.AddNewProduct(product);
        }

        public void RemoveProduct()
        {
            Console.Write("Enter ProductID to remove:");
            int ID = int.Parse(Console.ReadLine());
            storage.RemoveProductID(ID);
        }

        public void ListProductsByName()
        {
            var prodlist = storage.ProductsSortedByName();
            IView view = new ConsoleView(prodlist);
            view.RenderView();
        }
        public void ListProductsByPrice()
        {
            var prodlist = storage.ProductsSortedByPrice();
            IView view = new ConsoleView(prodlist);
            view.RenderView();
        }
    }
}
