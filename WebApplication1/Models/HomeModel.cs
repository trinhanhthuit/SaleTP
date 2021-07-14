using Sale.Business;
using System.Collections.Generic;
using Sale.Data;

namespace Sale.Models
{
    public class HomeRespone
    {
        public List<ProductModel> Products { get; set; }

        public AboutModel About { get; set; }
        public List<ServiceModel> Services { get; set; }
        public List<Image> LogoClients { get; set; }
        public List<TestimonialModel> Testimonials { get; set; }
        public List<EmployeelModel> Employees { get; set; }
        public WhyUSModel WhyUs { get; set; }
        public List<WhyUSDetailModel> WhyUsDetails { get; set; }
        public List<TextData> TextDatas { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public HomeRespone()
        {
            Services = new List<ServiceModel>();
            Testimonials = new List<TestimonialModel>();
            Employees = new List<EmployeelModel>();
            WhyUsDetails = new List<WhyUSDetailModel>();
            TextDatas = new List<TextData>();
            Categories = new List<CategoryModel>();
        }
    }
    public class AboutRespone
    {
        public AboutModel About { get; set; }
        public TextData TextData { get; set; }
    }
    public class ProductRespone
    {
        public List<CategoryTreeModel> Categories { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<ProductModel> SaleBestProducts { get; set; }
        public List<ProductModel> SalePromotioProducts { get; set; }
        public TextData TextData { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductDetailRespone
    {
        public ProductModel ProductDetail { get; set; }
        public List<ProductModel> ProductPromotions { get; set; }
        public List<ProductModel> ProductHots { get; set; }
        public TextData TextData { get; set; }
        public ProductDetailRespone()
        {
            ProductDetail = new ProductModel();
        }

    }
    public class ServiceRespone
    {
        public List<ServiceModel> Services { get; set; }
        public TextData TextData { get; set; }
    }
    public class ServiceDetailRespone
    {
        public ServiceModel Service { get; set; }
        public List<ServiceModel> ServiceRelations { get; set; }
        public TextData TextData { get; set; }
    }

    public class MenuRespone
    {
        public List<MenuModel> Menus { get; set; }
    }
    public class OrderRespone
    {
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
    }
}
