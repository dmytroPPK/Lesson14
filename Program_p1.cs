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
            List<Product> listOfProduct = Helper.GetListProduct();

            Program prog = new Program(listOfProduct, 2);
            prog.Init();
            prog.Menu();
        }
    }
}
