using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Sale.Data;

namespace WebApplication1.Utils
{
    public class Session : ISession
    {
        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }
        public string Language
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[Constant.LANGUAGE] == null)
                    return GetValueByKeyWord(Constant.LANGUAGE);
                else
                    return HttpContext.Current.Session[Constant.LANGUAGE].ToString();
            }
            set { HttpContext.Current.Session[Constant.LANGUAGE] = value; }

        }
        public bool IsLogin
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[Constant.IS_LOGIN] == null)
                    return false;
                else
                    return Convert.ToBoolean(HttpContext.Current.Session[Constant.IS_LOGIN].ToString());
            }
            set { HttpContext.Current.Session[Constant.IS_LOGIN] = value; }
        }
        public User User
        {
            get
            {
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[Constant.USER] == null)
                    return null;
                else
                    return (HttpContext.Current.Session[Constant.USER]) as User;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[Constant.USER] = value;
                }
            }
        }

        private string GetValueByKeyWord(string Keyword)
        {
            return WebConfigurationManager.AppSettings[Keyword];
        }
    }
}