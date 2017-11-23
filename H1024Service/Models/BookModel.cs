using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H1024Service.Models
{
    public class BookModel
    {

        public int bid { get; set; }
        public string bookname { get; set; }
        public string imgurl { get; set; }

        public int status { get; set; }

        public string date { get; set; }
    }
}