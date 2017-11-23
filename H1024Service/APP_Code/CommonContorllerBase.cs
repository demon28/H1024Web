
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;


namespace H1024Service.APP_Code
{
    [AoutLogin]
    public class CommonContorllerBase : TopContorllerBase
    {

        #region 登陆
        public bool IsAutoLogin
        {
            get { return AppConfig.AutoLogin; }
        }

        public string UserName
        {
            get
            {
                if (IsAutoLogin)
                {
                    Session["UserName"] = AppConfig.AutoName;

                }
                return string.IsNullOrEmpty(Session["UserName"] + "") ? "" : Session["UserName"].ToString();
            }
            set
            {
                _username = value;
                Session["UserName"] = _username;
            }
        }
        public bool IsLogin
        {
            get { return !string.IsNullOrEmpty(UserName); }
        }
        private string _username;

        public void LoginOut()
        {
            Session["UserName"] = null;

        }


        #endregion


    }
    public class AoutLoginAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public AoutLoginAttribute(bool checkLogin = true)
        {
            this.CheckLogin = checkLogin;
        }
        public bool CheckLogin { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!CheckLogin)
            {
                return;
            }
            string userName = filterContext.HttpContext.Session["UserName"] + string.Empty;
            if (string.IsNullOrEmpty(userName))
            {
                if (AppConfig.AutoLogin)
                {
                    filterContext.HttpContext.Session["UserName"] = AppConfig.AutoName;
                }
                else
                {
                    filterContext.Result = new RedirectResult("/login/index");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}