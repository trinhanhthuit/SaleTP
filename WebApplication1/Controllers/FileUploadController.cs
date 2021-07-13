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
using System.IO;
using System.Web;

namespace WebApplication1.Controllers
{
    public class FileUploadController : ApiController
    {
       
        public FileUploadController()
        {
            
        }
        const string FILE_PATH = @"D:\Samples\";

        [HttpPost]
        public HttpResponseMessage UploadFiles(string urlUpload)
        {
            var apiRespone = new ApiResponse { IsSuccess = false };
            //string urlUpload = System.Configuration.ConfigurationManager.AppSettings["UrlUpload"];
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath(urlUpload);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                string strFileName= HttpContext.Current.Request.Form["fileName"];
                string[] fileNames = strFileName.Split(',');
                //Loop through uploaded files  
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files[i];
                    if (httpPostedFile != null)
                    {
                        // Construct file save path  
                        //string fileName = httpPostedFile.FileName + Path.GetExtension(httpPostedFile.FileName);
                        string fileName = fileNames[i] + Path.GetExtension(httpPostedFile.FileName).Replace("jpeg", "jpg");

                        //Save the File.
                        httpPostedFile.SaveAs(path + fileName);

                    }
                }
            }

            ////Fetch the File.
            //HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            ////Fetch the File Name.
            //string fileName2 = HttpContext.Current.Request.Form["fileName"] + Path.GetExtension(postedFile.FileName);

            ////Save the File.
            //postedFile.SaveAs(path + fileName2);
            apiRespone.IsSuccess = true;
            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, apiRespone);
        }
        

    }
}
