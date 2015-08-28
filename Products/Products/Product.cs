namespace Products
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string name,int id, string description, float weight, Size size, int price, bool instock = false )
        {
            ProductName = name;
            ProductID = id;
            ProductDescription = description;
            Weight = weight;
            InStock = instock;
            ProductSize = size;
            Price = price;
        }
       
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public string ProductDescription { get; set; }
        public float Weight { get; set; }
        public bool InStock { get; set; }
        public int Price { get; set; }

        public Size ProductSize { get; set; }
    }
}