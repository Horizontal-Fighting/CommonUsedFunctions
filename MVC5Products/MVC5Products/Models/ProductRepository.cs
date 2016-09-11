//Created by: Rong Fan
//Email:rong.fan1031@gmail.com
//2016-9-10

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Products.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _indexId = 1;

        public ProductRepository()
        {
            products.Add(new Product { Id = 1, Name = "Tomato soup", Category = "Groceries", Price = 1.39M });
            products.Add(new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M });
            products.Add(new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M });
        }
        public IEnumerable<Product> GetAll()
        {
            return products;
        }
        public Product Get(int id)
        {
            return products.Find(p => p.Id == id);
        }
        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _indexId++;
            products.Add(item);
            return item;
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }
    }
}