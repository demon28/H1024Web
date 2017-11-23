using H1024Service.APP_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using H1024Service.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace H1024Service.Controllers
{
    public class HomeController : H1024Service.APP_Code.CommonContorllerBase
    {
        // GET: Home



        SqlHelper sqlhelper = new SqlHelper();


        public ActionResult Select(QueryBookModel model)
        {

            try {

                string sql = "select * from tb_book ";
                if (!string.IsNullOrEmpty(model.Str))
                {
                    sql += " where bookname like '%" + model.Str + "%'";
                }

                DataTable dt = sqlhelper.ExecuteQuery(sql);

                int pagecount = 0;
                DataTable dts = GetPagedTable(dt, model.PageIndex, model.PageSize, out pagecount);

                return ListResult(dts, model.PageIndex, model.PageSize, pagecount);
            }catch(Exception e)
            {
                return FailResult(e.ToString());
            }
            
            
         }



        public ActionResult Delete()
        {

            string bid = Request.QueryString["bid"];
            string sql = "select * from tb_book where bid=" + bid;

            DataTable dt = sqlhelper.ExecuteQuery(sql);
            string status = "0";
            if (dt.Rows[0]["status"].ToString() == "0")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }

            string sqlwhere = "update tb_book set status=" + status + " where bid=" + bid;

            if (sqlhelper.ExecuteNonQuery(sqlwhere) > 0)
            {
                return SuccessResult();
            }
            return FailResult();

        }

        [HttpPost]
        public ActionResult Add(BookModel model)
        {


            string filename = Guid.NewGuid().ToString();
            var path = string.Empty;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase  uploadFile = Request.Files[0] as HttpPostedFileBase;
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    filename += Path.GetExtension(uploadFile.FileName);
                    path = Path.Combine("/DataImg/", filename);
                    uploadFile.SaveAs(Server.MapPath(path));
                }

            }

            string sql = $"insert into tb_book values (null,0,'{model.bookname}','{path}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";

            if (sqlhelper.ExecuteNonQuery(sql) > 0)
            {
                return SuccessResult("添加成功");
            }

            return FailResult("添加失败");

        }


        public ActionResult Update(BookModel model)
        {

            string filename = Guid.NewGuid().ToString();
            var path = model.imgurl;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase uploadFile = Request.Files[0] as HttpPostedFileBase;
                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    filename += Path.GetExtension(uploadFile.FileName);
                    path = Path.Combine("/DataImg/", filename);
                    uploadFile.SaveAs(Server.MapPath(path));
                }

            }



            string sql = $"update tb_book set bookname='{model.bookname}',imgurl='{path}' where bid='{model.bid}'";

            if (sqlhelper.ExecuteNonQuery(sql) > 0)
            {
                return SuccessResult("修改成功");
            }

            return FailResult("修改失败");

        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BookList()
        {
            return View("BookList");
        }

        

        public ActionResult ListSelect()
        {
            string bid = Request.QueryString["bid"];

          
            string sql = "select * from tb_book_list where bid="+ bid;

            
            DataTable dt = sqlhelper.ExecuteQuery(sql);
         
            return ListResult(dt);
        }




        public ActionResult ListDelete()
        {
            string lid = Request.QueryString["lid"];
            string sql = "select * from tb_book_list where lid=" + lid;

            DataTable dt = sqlhelper.ExecuteQuery(sql);
            string status = "0";
            if (dt.Rows[0]["status"].ToString() == "0")
            {
                status = "1";
            }
            else
            {
                status = "0";
            }

            string sqlwhere = "update tb_book_list set status=" + status + " where lid=" + lid;

            if (sqlhelper.ExecuteNonQuery(sqlwhere) > 0)
            {
                return SuccessResult();
            }
            return FailResult();
        }


        public ActionResult ListReDelete()
        {
            string lid = Request.QueryString["lid"];
            
            string sqlwhere = "delete from tb_book_list where  lid=" + lid;

            if (sqlhelper.ExecuteNonQuery(sqlwhere) > 0)
            {
                return SuccessResult();
            }
            return FailResult();
        }


      
        [HttpPost]
        public ActionResult ListAdd()
        {

            string jsonstr = Request.Form["listmodel"].ToString();
            JavaScriptSerializer js = new JavaScriptSerializer();

            List<BookListModel> model = js.Deserialize<List<BookListModel>>(jsonstr);

      
            int count = 0;

            if (Request.Files.Count > 0)
            {

                for (int i = 0; i < model.Count; i++)
                {
                    string filename = Guid.NewGuid().ToString();
                    var path = string.Empty;

                    HttpPostedFileBase uploadFile = Request.Files[i] as HttpPostedFileBase;
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        filename += Path.GetExtension(uploadFile.FileName);
                        path = Path.Combine("/DataFile/", filename);
                        uploadFile.SaveAs(Server.MapPath(path));
                    }


                    string sql = $"insert into tb_book_list values (null,'{model[i].bid}','{model[i].status}','{model[i].listname}','{path}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}')";


                    if (sqlhelper.ExecuteNonQuery(sql) > 0)
                    {
                        count = count + 1;
                    }


                }

            }



            if (count == model.Count)
            {
                return SuccessResult("添加成功");
            }
            return FailResult("添加失败");

        }

    }

    public class ListAddModel {
      public   List<BookListModel> File { get; set; }



       
    }
}