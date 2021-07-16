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

namespace WebApplication1.Controllers
{
    public class LoginController : BaseController
    {
        IUser userBO;
        public LoginController()
        {
            this.userBO = this.userBO ?? new UserBO();
        }
        [HttpPost]
        public HttpResponseMessage CheckLogin([FromBody] LoginModel login)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new SessionRespone();
            string passWordSHA = this.HashSHA1(login.PassWord);
            var user = userBO.CheckLogin(login.UserName, passWordSHA);
            if (user != null)
            {
                _session.User = user;
                _session.IsLogin = true;
            }
            else
            {
                _session.User = null;
                _session.IsLogin = false;
                apiRespone.IsSuccess = false;
            }

            dataResults.User = _session.User;
            dataResults.IsLogin = _session.IsLogin;

            apiRespone.Data = dataResults;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }


    }
}
