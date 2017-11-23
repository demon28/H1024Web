using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H1024Service.APP_Code
{

    public static class AppConfig
        {
            public static bool AutoLogin
            {
                get
                {
                    string temp = System.Configuration.ConfigurationManager.AppSettings["autoLogin"];
                    return !string.IsNullOrEmpty(temp) && bool.Parse(temp);
                }
            }

            public static string AutoName
            {
                get
                {
                    string temp = System.Configuration.ConfigurationManager.AppSettings["AutoName"];
                    return string.IsNullOrEmpty(temp) ? "Near" : temp;
                }
            }


            public static string AutoPwd
            {
                get
                {
                    string temp = System.Configuration.ConfigurationManager.AppSettings["AutoPwd"];
                    return string.IsNullOrEmpty(temp) ? "1346888" : temp;
                }
            }




            public static int MaxNotifyCount
            {
                get
                {
                    string temp = System.Configuration.ConfigurationManager.AppSettings["MaxNotifyCount"];
                    if (string.IsNullOrEmpty(temp))
                        return 10;
                    int count = 10;
                    if (!int.TryParse(temp, out count))
                        return 10;
                    return count;
                }
            }


        

    }
}