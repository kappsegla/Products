using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Script.Serialization;

namespace ProductsMVC.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        [DisplayName("Article Number")]
        public long ArticleNumber { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    //Note that the DbSet properties on the context are marked as virtual. This will allow the mocking framework to derive from our context and overriding these properties with a mocked implementation.
    public class ProductDBContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //https://msdn.microsoft.com/en-us/data/jj591617.aspx
            //When using something other than conventions write code here
            //or use annotations https://msdn.microsoft.com/en-us/data/jj591583

        }
    }
}