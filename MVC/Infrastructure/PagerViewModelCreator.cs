using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Models;

namespace MVC.Infrastructure
{
    public static class PagerViewModelCreator<T> where T: class 
    {
        public static CustomPagerViewModel<T> GetPagerViewModel(IEnumerable<T> data, int page, int itemsPerPage)
        {
            var pager=new CustomPagerViewModel<T>();
            int count = data.Count();
            int numberOfPages = count / itemsPerPage;
            if (count % itemsPerPage != 0)
                numberOfPages++;
            pager.Data = data.Skip((page - 1)*itemsPerPage).Take(itemsPerPage);
            pager.NumberOfPages = numberOfPages;
            pager.CurrentPage = page;
            return pager;
        } 

    }
}