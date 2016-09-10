using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson14
{
    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public double Cash{ get; set; }
    }
    
    static class Store
    {
        public static IList<Product> GetListProduct()
        {
            var rand = new Random();
            var list = new List<Product>();
            for (int i = 1; i < 51; i++)
            {
                list.Add(new Product
                {
                    ID = i,
                    Name = (i % 2 == 0) ? $"New Product {i}" : $"Product {i}",
                    Price = rand.Next(10,1500)* 1.045,
                    Discount = rand.Next(51),
                    Quantity = rand.Next(10)
                });
            }
            
            return list;
        }
    }
}
