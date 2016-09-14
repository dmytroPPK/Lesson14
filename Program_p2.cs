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
        public int ItemsOnPage => itemsOnPage; 
        public  List<Product> ListOfProduct { get; }
        private List<dynamic> correctList;
        public int CountOfPages => countOfPages;

        // fields for pagination
        private int countOfPages;
        private int firstItemOnPage;
        private int itemsOnPage;
        private int currentPage;
        private int pageIndex;
        private string menustr;

        public Program(List<Product> listOfProduct_, int defaultItemsOnPage_ = 2)
        {
            ListOfProduct = listOfProduct_;
            this.itemsOnPage = defaultItemsOnPage_;
            Helper.ChangeColor(ConsoleColor.White);
        }
        private IEnumerable<dynamic> ListCorrection()
        {
            var newList = from data in ListOfProduct
                          where data.Price < 1000
                          where data.Quantity > 2
                          where data.Name.Contains("New")
                          where data.Discount > 0
                          select new
                          {
                              ID = data.ID,
                              Name = data.Name,
                              Cash = (data.Quantity*data.Price)*((100D - data.Discount)/100)
                          };
            return newList;
        }
        public void Init()
        {
            this.correctList = ListCorrection().ToList();
            this.countOfPages = (int)Math.Ceiling(correctList.Count()*1.0/ itemsOnPage);
            Console.WriteLine("items on page = " + itemsOnPage);
            Console.WriteLine("items in list = "+ correctList.Count());
            Console.WriteLine("countOfPages of page = " + this.countOfPages);
            Console.WriteLine(correctList.GetType());
        }
        private void ViewItemsOnCurrentPage(int pageIndex_ )
        {
            this.pageIndex = (pageIndex_<1 || pageIndex_ > countOfPages)? countOfPages : pageIndex_;
            this.firstItemOnPage = (pageIndex * ItemsOnPage) - (ItemsOnPage -1);

            // get items
            string strData;
            for (int i = firstItemOnPage, j = 1; i < firstItemOnPage + ItemsOnPage; i++, j++)
            {
                if(i <= this.correctList.Count)
                {
                    strData = $"{j,3}.  ID = {this.correctList[i-1].ID,3}  ,Name = {this.correctList[i - 1].Name,-15}  ,Cash = {this.correctList[i - 1].Cash,-10}";
                    Console.WriteLine(strData);
                }
            }
            this.ShowChainPages();
            Console.WriteLine();
        }
        public void Menu()
        {
            menustr = InitMenuText();
            var enteredvalue = "";
            while (true)
            {
                Console.Clear();
                Console.Write(menustr);
                enteredvalue = Console.ReadLine();
                switch (enteredvalue.Trim().ToLower())
                {
                    case "1":
                        ShowWithPagination();
                        break;

                    case "2":
                        ChangeItemsOnPage();
                        break;

                    case "3":
                        MatchList();
                        break;

                    case "exit":
                        break;

                    default:
                        continue;
                }
                break;
            }
        }

        private void MatchList()
        {
            var @str = "";
            while (true)
            {
                Console.Write("  - Введите слова через запятую: ");
                @str = Console.ReadLine();
                if (!@str.Contains(','))
                {
                    Helper.ChangeColor(ConsoleColor.Red);
                    Console.WriteLine("<,> is used as separator !!!");
                    Helper.ChangeColor(ConsoleColor.White);
                    continue;
                }
                break;
            }
            List<dynamic> query = (from item in this.correctList
                                  where (item.Name as string).MyContains(@str)
                                  select item).ToList();
            if(query.Count > 0)
            {
                Console.WriteLine("\n\t--- Matched List ---\n");
                foreach (dynamic item in query)
                {
                    Console.WriteLine($"ID {item.ID,-5} Name {item.Name,-20}");
                }
            }else
            {
                Helper.ChangeColor(ConsoleColor.Yellow);
                Console.WriteLine("\t--- Oops! No matches ---");
                Helper.ChangeColor(ConsoleColor.White);
            }
            Console.ReadKey();
            this.Menu();
        }

        private void ChangeItemsOnPage()
        {
            var items = 0;
            while (true)
            {
                try
                {
                    Console.Write("  - Enter quatity of items on page: ");
                    var value = Console.ReadLine();
                    items = Int32.Parse(value);
                    if (items < 1) throw new Exception("Alert");
                    break;
                }
                catch
                {
                    Helper.ChangeColor(ConsoleColor.Red);
                    Console.WriteLine("Incorrect data for parsing");
                    Helper.ChangeColor(ConsoleColor.White);
                }
            }
            this.itemsOnPage = items;
            this.Init();
            this.Menu();
        }

        private void ShowWithPagination()
        {
            this.currentPage = 1;
            ViewItemsOnCurrentPage(this.currentPage);
            while (true)
            {
                try
                {                   
                    Console.Write("  - Enter a index of page or <q> to exit: ");
                    var value = Console.ReadLine();
                    if (value.ToLower().Trim() == "q") break;
                    var index = Int32.Parse(value);
                    if (index < 1 || index > this.countOfPages) throw new Exception("Boundary of index");
                    this.currentPage = index;
                    Console.Clear();
                    Console.WriteLine(this.InitMenuText());
                    ViewItemsOnCurrentPage(this.currentPage);
                }
                catch
                {
                    Helper.ChangeColor(ConsoleColor.Red);
                    Console.WriteLine("Incorrect data for parsing or boundary of index");
                    Helper.ChangeColor(ConsoleColor.White);
                }
            }
            this.Menu();
        }

        private string InitMenuText()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("1. Получить товары с: Price > 1000, Quantity >2, Name contains 'New'").AppendLine();
            builder.Append("2. Изменить к-сто результатов на странице").AppendLine();
            builder.Append("3. Поиск данных по ключевым словам").AppendLine();
            builder.Append("Для выхода наберите < exit >").AppendLine();
            builder.Append("\n\t\tВаше действие:  ");
            return builder.ToString();
        }
        private void ShowChainPages()
        {
            Console.Write("\n\t  ");
            for (int i = 1; i <= this.countOfPages; i++)
            {
                if(i == this.currentPage)
                {
                    Helper.ChangeColor(ConsoleColor.Cyan);
                    Console.Write("<"+i+ "> ");
                    Helper.ChangeColor(ConsoleColor.White);
                }
                else
                {
                    Console.Write(i + " ");
                }
                
            }
            Console.WriteLine();
        }

    }
    
}
