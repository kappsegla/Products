﻿using System;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Product
    {
            public int ID { get; set; }
            public string Title { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Genre { get; set; }
            public decimal Price { get; set; }
    }
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}