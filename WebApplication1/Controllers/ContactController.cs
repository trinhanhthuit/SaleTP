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
using Sale.Data;

namespace WebApplication1.Controllers
{
    public class ContactController : ApiController
    {
       

        IContact contactBO;
        ITextData textDataBO;
        public ContactController()
        {
            this.contactBO = this.contactBO ?? new ContactBO();
            this.textDataBO = this.textDataBO ?? new TextDataBO();
        }

        [HttpGet]
        //[EnableCors("AllowOrigin")]
        public HttpResponseMessage GetData(string langId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new AboutRespone();
            //dataResults.About = aboutBO.GetData(langId);
            //dataResults.TextData = textDataBO.GetData(langId, "ABOUT")[0];
            apiRespone.Data = dataResults;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }

        //[AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage SaveContact([FromBody] Contact contact)
        {
            var apiRespone = new ApiResponse { IsSuccess = false };
            //var dataResults = new AboutRespone();
            //dataResults.About = aboutBO.GetData(langId);
            //dataResults.TextData = textDataBO.GetData(langId, "ABOUT")[0];
            contact.CreatedDate = DateTime.Now;
            var isResult = contactBO.Add(contact);
            apiRespone.IsSuccess = isResult;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        


    }
}
