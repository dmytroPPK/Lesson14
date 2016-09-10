using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson14
{
    partial class Program
    {
        

        static void Main(string[] args)
        {
            IList<Product> listOfProduct = Store.GetListProduct();
            Program prog = new Program(listOfProduct, 5);
            prog.Init();
            prog.Menu(1);
            Console.WriteLine(new String('-', 30));
            foreach (dynamic item in prog.ListCorrection())
            {
                Console.WriteLine(  item.ID);
            }
            Console.WriteLine(new String('-',30));
            foreach (var item in Store.GetListProduct())
            {
                Console.WriteLine($"ID = {item.ID}, Discont = {item.Discount},   Price = {item.Price},  Quantity = {item.Quantity}");
            }
            


            //var data = new List<dynamic>()
            //{
            //    new {ID =1, Name =32, FNAME =3},
            //    new {ID =2, Name =28, FNAME =45},
            //    new {ID =3, Name =24, FNAME =15},
            //    new {ID =4, Name =23, FNAME =74},
            //    new {ID =5, Name =12, FNAME =10}
            //};
            //var newList = (
            //                    from v in data
            //                    //where v.ID > 2
            //                     where v.Name > 12
            //                    select new {SName = v.Name, PName = v.FNAME }
            //                  ).ToList();
            //for (int i = 0; i < newList.Count; i++)
            //{
            //    Console.WriteLine(newList[i].SName);
            //}

            Console.ReadKey();
        }
    }
}
