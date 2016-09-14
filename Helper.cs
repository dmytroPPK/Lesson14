using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lesson14
{
    static class Helper
    {
        public static bool MyContains(this string @str,string dataForParse )
        {
            if (dataForParse == null || dataForParse.Length == 0)
            {
                return false;
            }
            var data = dataForParse.Trim().Split(',');
            
            foreach (string item in data)
            {
                if (item.Trim() == "") continue;
                if (@str.Contains(item.Trim())) return true;
            }
            return false;
        }

        public static List<Product> GetListProduct()
        {
            var rand = new Random();
            var list = new List<Product>();
            for (int i = 1; i < 101; i++)
            {
                char a1 = (char)rand.Next(65,90);
                char a2 = (char)rand.Next(97,122);
                char a3 = (char)rand.Next(97, 122);
                char a4 = (char)rand.Next(97, 122);
                char a5 = (char)rand.Next(97, 122);
                list.Add(new Product
                {
                    ID = i,
                    Name = (i % 2 == 0) ? $"New {a1}{a2}{a3}{a4}{a5}" : $"Product {a1}{a2}{a3}{a4}{a5}",
                    Price = rand.Next(10, 1500) * 1.045,
                    Discount = rand.Next(51),
                    Quantity = rand.Next(10)
                });
            }

            return list;
        }
        public static void ChangeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}
