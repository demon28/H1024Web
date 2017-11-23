using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H1024Service.Models
{
    public class QueryBookModel
    {

      public  string Str { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}