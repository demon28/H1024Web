using H1024Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H1024Service.APP_Code;

namespace H1024Service.Controllers
{
    public class LoginController : TopContorllerBase
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [AoutLoginAttribute(false)]
        public JsonResult Login(LoginModel model)
        {
            if (model.UserName!=AppConfig.AutoName || model.Password!=AppConfig.AutoPwd)
            {
                return FailResult("登录失败！");
            }
            Session["UserName"] = model.UserName;
            return  SuccessResult("登录成功");



        }


        public ActionResult LoginOut()
        {
            Session["UserName"] = null;
            return Redirect("/Login/index");
        }



    }
}