using System.Web;
using System.Web.Mvc;
using System.Data;
using H1024Service.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using H1024Service.APP_Code;

namespace H1024Service.Controllers
{
    public class APIController : TopContorllerBase
    {
        // GET: API
        public ActionResult Index()
        {
            return View();
        }



        SqlHelper sqlhelper = new SqlHelper();


        public ActionResult Select(QueryBookModel model)
        {

            try
            {

                string sql = "select * from tb_book ";
                if (!string.IsNullOrEmpty(model.Str))
                {
                    sql += " where bookname like '%" + model.Str + "%'";
                }

                DataTable dt = sqlhelper.ExecuteQuery(sql);

                int pagecount = 0;
                DataTable dts = GetPagedTable(dt, model.PageIndex, model.PageSize, out pagecount);

                return ListResult(dts, model.PageIndex, model.PageSize, pagecount);
            }
            catch (System.Exception e)
            {
                return FailResult(e.ToString());
            }


        }



        public ActionResult ListSelect()
        {
            string bid = Request.QueryString["bid"];


            string sql = "select * from tb_book_list where bid=" + bid;


            DataTable dt = sqlhelper.ExecuteQuery(sql);

            return ListResult(dt);
        }

    }
}