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
    public class AboutController : ApiController
    {
        IAbout aboutBO;
        ITextData textDataBO;
        public AboutController()
        {
            this.aboutBO = this.aboutBO ?? new AboutBO();
            this.textDataBO = this.textDataBO ?? new TextDataBO();
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public HttpResponseMessage GetData(string langId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new AboutRespone();
            dataResults.About = aboutBO.GetData(langId);
            dataResults.TextData = textDataBO.GetData(langId, "ABOUT")[0];
            apiRespone.Data = dataResults;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }


    }
}
