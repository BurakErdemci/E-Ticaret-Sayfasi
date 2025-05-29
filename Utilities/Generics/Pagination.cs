using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Generics
{
    public class Pagination<T>
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> CurrentData { get; set; } = [];
        public int Limit { get; set; }
        public int StartClamp { get; set; }
        public  int   EndClamp { get; set; }


        public Pagination(IEnumerable<T> data, int currentPage, int limit , int clampLimit=3)
        {
            CurrentPage = currentPage;
            Limit = limit;
            TotalPages = (int)Math.Ceiling(data.Count() / (double)Limit);
            StartClamp= currentPage-clampLimit<1?1:currentPage-clampLimit;
            StartClamp= currentPage+clampLimit>TotalPages?TotalPages:currentPage-clampLimit;
            CurrentData = data.Skip((currentPage - 1) * limit).Take(limit);
        }
    }
}
