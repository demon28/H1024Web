using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H1024Service.Models
{
    public class BookListModel
    {
        public int lid { get; set; }
        public int bid { get; set; }
        public int status { get; set; }
        public string listname { get; set; }
        public string fileurl { get; set; }
        public string createdate { get; set; }
    }


    public  class SelectList
    {
        public int id { get; set; }

        public string name { get; set; }

        public string text { get; set; }
    }
}