using H1024Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H1024Service.APP_Code
{
    public class TopContorllerBase : Controller
    {
        protected JsonResult SuccessResult(string message = "")
        {

            JsonBaseModel basemodel = new JsonBaseModel();
            basemodel.Success = true;
            basemodel.Message = message;

            return Json(basemodel, JsonRequestBehavior.AllowGet);

        }

        protected JsonResult FailResult(string message = "")
        {

            JsonBaseModel basemodel = new JsonBaseModel();
            basemodel.Success = false;
            basemodel.Message = message;

            var result = new JsonResult();
            result.Data = basemodel;
            return Json(basemodel, JsonRequestBehavior.AllowGet);


        }

        protected JsonResult ListResult(object obj,int pageindex, int pagesize, int pagecount)
        {

            var model = new
            {
                Success = true,
                Message = "",
                Pageindex = pageindex,
                PageSize = pagesize,
                PageCount= pagecount,
                Content = obj

            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        protected JsonResult ListResult(DataTable table, int pageindex, int pagesize, int pagecount)
        {
            ArrayList list = new ArrayList();
            Dictionary<string, object> model = new Dictionary<string, object>();
          
            foreach (DataRow item in table.Rows)
            {
                model = new Dictionary<string, object>();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DataColumn c = table.Columns[i];
                    model.Add(c.ColumnName,item[i]);
                }
                list.Add(model);
            }

            return ListResult(list,  pageindex, pagesize,  pagecount);
        }

        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize ,out int pagecount)//PageIndex表示第几页，PageSize表示每页的记录数
        {

            if (dt.Rows.Count%PageSize>0)
            {
                pagecount = dt.Rows.Count / PageSize + 1;
            }
            else 
            {
                pagecount = dt.Rows.Count / PageSize ;
            }



            if (PageIndex == 0)
            {
                return dt;//0页代表每页数据，直接返回
               
            }
            DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            {
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt
            }
            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }


        protected JsonResult ListResult(DataTable table)
        {
            ArrayList list = new ArrayList();
            Dictionary<string, object> model = new Dictionary<string, object>();

            foreach (DataRow item in table.Rows)
            {
                model = new Dictionary<string, object>();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DataColumn c = table.Columns[i];
                    model.Add(c.ColumnName, item[i]);
                }
                list.Add(model);
            }

            return ListResult(list);
        }



        protected JsonResult ListResult(object obj)
        {
            var model = new
            {
                Success = true,
                Message = "",
                Content = obj

            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}