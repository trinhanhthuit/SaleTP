using Sale.Data;
using System.Collections.Generic;

namespace Sale.Business
{
    public class ProductModel : Product
    {
        public System.Guid ProductLangID { get; set; }
        public string ProductName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ShortContent { get; set; }
        public string LongContent { get; set; }
        public string LangID { get; set; }
        public string ImagePath { get; set; }
        public string ParentCode { get; set; }
        public List<Image> Images { get; set; }
    }
    public class CategoryModel : Category
    {
        public string CategoryName { get; set; }
        public string LangID { get; set; }
        public string CategoryNameParent { get; set; }

    }
    public class AboutModel : About
    {
        public string LangID { get; set; }
        public string AboutName { get; set; }
        public string AboutTitle { get; set; }
        public string AboutContent { get; set; }
        public string AboutContent2 { get; set; }
        public string AboutSubContent { get; set; }
        public string AboutContent3 { get; set; }
    }
    public class ServiceModel : Service
    {
        public string LangID { get; set; }
        public string ServiceName { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string LongContent { get; set; }
        public string ImagePath { get; set; }
    }
    public class TestimonialModel : TestimonialLang
    {
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
    public class EmployeelModel : Employee
    {
        public string LangID { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
    public class WhyUSModel : WhyU
    {
        public string LangID { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public string Content { get; set; }

    }
    public class WhyUSDetailModel : WhyUSDetail
    {
        public string LangID { get; set; }
        public string Title { get; set; }
        public string SubContent { get; set; }
        public string Content { get; set; }

    }

    public class CategoryTreeModel : CategoryModel
    {
        //public CategoryModel Catergory { get; set; }
        public List<CategoryModel> SubCategories { get; set; }

    }
    public class ProductBackEndRespone
    {
        public List<ProductModel> Products { get; set; }
        public int TotalRows { get; set; }
    }
    public class CategoryBackEndRespone
    {
        public List<CategoryModel> Categories { get; set; }
        public int TotalRows { get; set; }
    }
    public class ProductImageBodyModel
    {
        public ProductModel Product { get; set; }
        public List<Image> Images { get; set; }
    }
    public class ServiceImageModel
    {
        public ServiceModel Service { get; set; }
        public List<Image> Images { get; set; }
    }
    public class DataBackEndRespone
    {
        public object Results { get; set; }
        public int TotalRows { get; set; }
    }

    public class MenuModel : Menu
    {
        public List<SubMenu> SubMenu1 { get; set; }
        public MenuModel()
        {
            SubMenu1 = new List<SubMenu>();
        }
    }

    public class SubMenu : Menu
    {
        public List<Menu> SubMenu2 { get; set; }
        public SubMenu()
        {
            SubMenu2 = new List<Menu>();
        }
    }
    public class OrderModel 
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Customer Customer { get; set; }


    }
}
