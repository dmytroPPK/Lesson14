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
        public int ItemsOnPage => itemsOnPage;    //{ get { return defaultItemsOnPage; } set { defaultItemsOnPage = value; } }
        public IList<Product> ListOfProduct { get; }
        private IEnumerable<dynamic> correctList;
        public int CountOfPages => countOfPages;

        // fields for pagination
        private int countOfPages;
        private int firstItemOnPage;
        private int itemsOnPage;
        private int currentPage;
        private int pageIndex;
        public Program(IList<Product> listOfProduct_, int defaultItemsOnPage_ = 2)
        {
            ListOfProduct = listOfProduct_;
            this.itemsOnPage = defaultItemsOnPage_;

            
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
                              Cash = (data.Quantity*data.Price)*((100 - data.Discount)/100)
                          };
            newList = newList.ToList();
            return newList;
        }
        public void Init()
        {
            this.correctList = ListCorrection();
            this.countOfPages = (int)Math.Ceiling(correctList.Count()*1.0/ itemsOnPage);
            Console.WriteLine("items on page = " + itemsOnPage);
            Console.WriteLine("items in list = "+ correctList.Count());
            Console.WriteLine("countOfPages of page = " + this.countOfPages);
            Console.WriteLine(correctList.GetType());
        }
        private void ViewItemsOnCurrentPage(int pageIndex_ = 1)
        {

            dynamic listProduct = this.correctList;
            pageIndex = (pageIndex_<1 || pageIndex_ > countOfPages)? countOfPages : pageIndex_;
            this.firstItemOnPage = (pageIndex * ItemsOnPage) - (ItemsOnPage -1);

            // get items
            string strData;
            for (int i = firstItemOnPage, j = 1; i < firstItemOnPage + ItemsOnPage; i++, j++)
            {
                if(i <= listProduct.Count)
                {
                    strData = $"ID = {listProduct[i-1].ID,3}  ,Name = {listProduct[i - 1].Name,-15}  ,Cash = {listProduct[i - 1].Cash,-10}  ,j = {j,3}";
                    Console.WriteLine(strData);
                }
            }
        }
        public void Menu(int index)
        {
            ViewItemsOnCurrentPage(index);
        }

    }
    
}
