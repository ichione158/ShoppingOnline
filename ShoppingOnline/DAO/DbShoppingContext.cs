using ShoppingOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingOnline.DAO
{
    public class DbShoppingContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbShoppingContext() : base("shoppingConnectionString")
        {

        }
    }
}