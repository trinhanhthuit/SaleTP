using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Microsoft.AspNetCore.Mvc;
using Sale.Models;
using Sale.Business;
using Microsoft.AspNetCore.Cors;
using WebApplication1.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class BaseController : ApiController
    {
        #region Variables
        protected ISession _session;
        #endregion
        public BaseController(ISession sessions)
        {
            _session = sessions;
        }
        public BaseController() : this(new Session())
        {
            this._session = this._session ?? new Session();
        }
        public string HashSHA1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        protected void Initialize(RequestContext requestContext)
        {
            //base.Initialize(requestContext);
        }


    }
}
