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
using System.IdentityModel.Tokens.Jwt;
using log4net;

namespace WebApplication1.Controllers
{
    public class ProductController : ApiController
    {
        IProduct productBO;
        ITextData textDataBO;
        IImage imageBO;
        ICategory categoryBO;
        IService serviceBO;
        IAbout aboutBO;
        IContact contactBO;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ProductController()
        {
            this.productBO = this.productBO ?? new ProductBO();
            this.textDataBO = this.textDataBO ?? new TextDataBO();
            this.imageBO = this.imageBO ?? new ImageBO();
            this.categoryBO = this.categoryBO ?? new CategoryBO();
            this.serviceBO = this.serviceBO ?? new ServiceBO();
            this.aboutBO = this.aboutBO ?? new AboutBO();
            this.contactBO = this.contactBO ?? new ContactBO();
        }
        [HttpGet]
        public HttpResponseMessage GetImage(Guid pathId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = imageBO.GetList(m => m.PathID == pathId).ToList();
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        #region Product
        [HttpGet]
        public HttpResponseMessage GetInitData(string langId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new ProductRespone();
            var categories = productBO.GetCategoryTree(langId);
            apiRespone.Data = categories;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetProduct(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = productBO.GetProduct(searchString, isActive, langId, pageIndex, pageSize);
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveProduct([FromBody] ProductImageBodyModel model)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var product = new Product();
                product.ProductID = model.Product.ProductID;
                product.CreatedDate = DateTime.Now;
                product.LinkCode = model.Product.LinkCode;
                product.ProductCode = model.Product.ProductCode;
                product.SalePrice = model.Product.SalePrice;
                product.DiscountPrice = model.Product.DiscountPrice;
                product.IsHot = model.Product.IsHot;
                product.IsActive = model.Product.IsActive;
                product.ImagePath1 = model.Product.ImagePath1;
                product.ImagePath2 = model.Product.ImagePath2;
                product.CategoryID = model.Product.CategoryID;
                var productLang = new ProductLang();
                productLang.LangID = model.Product.LangID;
                productLang.ProductID = model.Product.ProductID;
                productLang.ProductName = model.Product.ProductName;
                productLang.ShortContent = model.Product.ShortContent;
                productLang.LongContent = model.Product.LongContent;


                //apiRespone.IsSuccess = isResult;
                var isResult = productBO.SaveProduct(product, productLang, model.Images);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteProduct([FromBody] Product product)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var isResult = productBO.DeleteProduct(product.ProductID);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion Product

        #region Category
        [HttpGet]
        public HttpResponseMessage GetInitDataCategory(string langId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new ProductRespone();
            var categories = productBO.GetCategoryTree(langId);
            apiRespone.Data = categories;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetCategory(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = categoryBO.GetCategory(searchString, isActive, langId, pageIndex, pageSize);
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage SaveCategory([FromBody] CategoryModel model)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var category = new Category();
                category.CategoryID = model.CategoryID;
                category.CreatedDate = DateTime.Now;
                category.CategoryCode = model.CategoryCode;
                category.CreatedBy = null;
                category.ModifiBy = null;
                category.ModifyDate = model.ModifyDate;
                category.IsActive = model.IsActive;
                category.ParentID = model.ParentID;
                category.Ids = "." + category.CategoryID + "." + category.ParentID;
                var categoryLang = new CategoryLang();
                categoryLang.CategoryID = model.CategoryID;
                categoryLang.LangID = model.LangID;
                categoryLang.CategoryName = model.CategoryName;

                //apiRespone.IsSuccess = isResult;
                var isResult = categoryBO.SaveCategory(category, categoryLang);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "SuSccessfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteCategory([FromBody] Category category)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var isResult = categoryBO.DeleteCategory(category.CategoryID);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion Category

        #region Service
        [HttpGet]
        public HttpResponseMessage GetInitDataService(string langId)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            var dataResults = new ProductRespone();
            var categories = productBO.GetCategoryTree(langId);
            apiRespone.Data = categories;
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        [HttpGet]
        public HttpResponseMessage GetService(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = serviceBO.GetService(searchString, isActive, langId, pageIndex, pageSize);
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage SaveService([FromBody] ServiceImageModel model)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var service = new Service();
                service.ServiceID = model.Service.ServiceID;
                service.CreatedDate = DateTime.Now;
                service.LinkCode = model.Service.LinkCode;
                service.IsActive = model.Service.IsActive;
                var serviceLang = new ServiceLang();
                serviceLang.LangID = model.Service.LangID;
                serviceLang.ServiceID = model.Service.ServiceID;
                serviceLang.ServiceName = model.Service.ServiceName;
                serviceLang.Title = model.Service.Title;
                serviceLang.ShortContent = model.Service.ShortContent;
                serviceLang.LongContent = model.Service.LongContent;
                var isResult = serviceBO.SaveService(service, serviceLang, model.Images);
                apiRespone.IsSuccess = isResult;
                if (apiRespone.IsSuccess)
                {
                    apiRespone.Message = "Thành công!";
                }
                else
                {
                    apiRespone.Message = "";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteService([FromBody] Service service)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var isResult = serviceBO.DeleteService(service.ServiceID);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion Service

        #region About
        [HttpGet]
        public HttpResponseMessage About(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = aboutBO.GetAbout(searchString, isActive, langId, pageIndex, pageSize);
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage About([FromBody] AboutModel model)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var about = new About();
                about.AboutID = model.AboutID;
                about.CreatedDate = DateTime.Now;
                about.CreatedBy = null;
                about.ModifyBy = null;
                about.ModifyDate = DateTime.Now;
                about.IsActive = model.IsActive;
                var aboutLang = new AboutLang();
                aboutLang.AboutID = model.AboutID;
                aboutLang.LangID = model.LangID;
                aboutLang.AboutName = model.AboutName;
                aboutLang.AboutTitle = model.AboutTitle;
                aboutLang.AboutSubContent = model.AboutSubContent;
                aboutLang.AboutContent = model.AboutContent;
                aboutLang.AboutContent2 = model.AboutContent2;
                aboutLang.AboutContent3 = model.AboutContent3;

                //apiRespone.IsSuccess = isResult;
                var isResult = aboutBO.SaveAbout(about, aboutLang);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteAbout([FromBody] About about)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                var isResult = aboutBO.DeleteAbout(about.AboutID);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion About

        #region Contact
        [HttpGet]
        public HttpResponseMessage Contact(string searchString, string fromDate, string toDate, int status, int pageIndex, int pageSize)
        {
            var apiRespone = new ApiResponse { IsSuccess = true };
            apiRespone.Data = contactBO.GetData(searchString, fromDate, toDate, status, pageIndex, pageSize);
            apiRespone.Message = "Successfuly!";
            var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Contact([FromBody] Contact contact)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                
                contact.CreatedDate = DateTime.Now;
                var isResult = contactBO.Save(contact);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteContact([FromBody] Contact contact)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                contact.Status = (int)EnumStatusContact.Delete;
                contact.ModifyDate = DateTime.Now;
                var isResult = contactBO.Save(contact);
                apiRespone.IsSuccess = isResult;
                if (isResult)
                {
                    apiRespone.Message = "Successfuly!";
                }
                else
                {
                    apiRespone.Message = "Unsuccessfuly!";
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion Contact

        ///test thay đổi
        ///
    }
}
