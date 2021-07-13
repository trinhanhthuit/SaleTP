using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Microsoft.AspNetCore.Mvc;
using Sale.Models;
using Sale.Business;
using Sale.Data;
using log4net;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IProduct productBO;
        IEmployee employeeBO;
        IWhyUS whyUSBO;
        ITextData textDataBO;
        IAbout aboutBO;
        IContact contactBO;
        IMenu menuBO;
        IImage imageBO;
        IOrder orderBO;
        IProvince provinceBO;
        IDistrict districtBO;
        public ValuesController()
        {
            this.productBO = this.productBO ?? new ProductBO();
            this.employeeBO = this.employeeBO ?? new EmployeeBO();
            this.whyUSBO = this.whyUSBO ?? new WhyUSBO();
            this.textDataBO = this.textDataBO ?? new TextDataBO();
            this.aboutBO = this.aboutBO ?? new AboutBO();
            this.contactBO = this.contactBO ?? new ContactBO();
            this.menuBO = this.menuBO ?? new MenuBO();
            this.imageBO = this.imageBO ?? new ImageBO();
            this.orderBO = this.orderBO ?? new OrderBO();
            this.provinceBO = this.provinceBO ?? new ProvinceBO();
            this.districtBO = this.districtBO ?? new DistrictBO();
        }
        [HttpGet]
        public HttpResponseMessage GetMenuData(string langId)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new MenuRespone();
                dataResults.Menus = menuBO.GetMenuData(langId);
                foreach (var menu in dataResults.Menus)
                {
                    if (menu.MenuType == "PRODUCT")
                    {
                        menu.SubMenu1 = menuBO.GetMenuProduct(langId);
                    }

                }
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetHomeData(string langId)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new HomeRespone();
                dataResults.Products = productBO.GetData(langId, null);
                dataResults.Categories = productBO.GetCategory(langId, 0);
                dataResults.About = aboutBO.GetData(langId);
                dataResults.Services = productBO.GetService(langId);
                dataResults.LogoClients = productBO.GetImage(1, null);
                dataResults.Testimonials = productBO.GetTestimonial(langId);
                dataResults.Employees = employeeBO.GetData(3, langId);
                dataResults.WhyUs = whyUSBO.GetData(langId);
                dataResults.WhyUsDetails = whyUSBO.GetDataDetail(3, langId);
                dataResults.TextDatas = textDataBO.GetData(langId, "HOME");
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetServiceData(string langId)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new ServiceRespone();
                dataResults.Services = productBO.GetService(langId);
                dataResults.TextData = textDataBO.GetData(langId, "SERVICES")[0];
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetServiceDetailData(string langId, string linkCode)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new ServiceDetailRespone();
                var services = productBO.GetService(langId);
                dataResults.ServiceRelations = services.Where(m => m.LinkCode != linkCode).ToList();
                dataResults.Service = services.Where(m => m.LinkCode == linkCode).FirstOrDefault();
                dataResults.TextData = textDataBO.GetData(langId, "SERVICES")[0];
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductDetail(string langId, string linkCode)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new ProductDetailRespone();
                var products = productBO.GetData(langId, null);
                dataResults.ProductDetail = products.Where(m => m.LinkCode == linkCode).FirstOrDefault();
                if (dataResults.ProductDetail != null)
                {
                    dataResults.ProductDetail.Images = imageBO.GetList(m => m.PathID == dataResults.ProductDetail.ProductID).ToList();
                }
                dataResults.ProductPromotions = products.Where(m => m.SalePrice > m.DiscountPrice).ToList();
                dataResults.ProductHots = products.Where(m => m.IsHot == true).ToList();
                dataResults.TextData = textDataBO.GetData(langId, "PRODUCT")[0];
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetDataProduct(string langId, string cateid)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new ProductRespone();
                dataResults.Categories = productBO.GetCategoryTree(langId);
                dataResults.Products = productBO.GetData(langId, cateid);
                dataResults.SaleBestProducts = productBO.GetProductSaleBest(langId, 10);
                dataResults.SalePromotioProducts = productBO.GetProductPromotion(langId, 10);
                dataResults.TextData = textDataBO.GetData(langId, "PRODUCT")[0];
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #region About
        [HttpGet]
        public HttpResponseMessage GetDataAbout(string langId)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion

        #region Contact
        [HttpGet]
        public HttpResponseMessage GetDataContact(string langId)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveContact([FromBody] Contact contact)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        #endregion
        #region Order
        [HttpGet]
        public HttpResponseMessage GetDataOrder()
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = true };
                var dataResults = new OrderRespone();
                dataResults.Provinces = provinceBO.GetList(m=>m.IsActive).ToList();
                dataResults.Districts = districtBO.GetList(m => m.IsActive).ToList();
                apiRespone.Data = dataResults;
                apiRespone.Message = "Successfuly!";
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
        public HttpResponseMessage SaveOrder([FromBody] Order order)
        {
            try
            {
                var apiRespone = new ApiResponse { IsSuccess = false };
                //orderModel.Order.CreatedDate = DateTime.Now;
                //var isResult = orderBO.SaveOrder(orderModel.Order, orderModel.OrderDetails, orderModel.Customer);
                //apiRespone.IsSuccess = isResult;
                apiRespone.Message = "Successfuly!";
                var response = Request.CreateResponse(HttpStatusCode.OK, apiRespone);
                return response;
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion 
    }
}
