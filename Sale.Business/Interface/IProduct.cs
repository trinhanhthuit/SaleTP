using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IProduct : IRepository<Product>
    {
        List<ProductModel> GetData(string langId, string cateid);
        List<CategoryModel> GetCategory(string langId, int parentId);
        List<ServiceModel> GetService(string langId);
        List<Image> GetImage(int categoryId, string pathId);
        List<TestimonialModel> GetTestimonial(string langId);
        List<CategoryTreeModel> GetCategoryTree(string langId);
        ProductBackEndRespone GetProduct(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool SaveProduct(Product product, ProductLang productLg, List<Image> images);
        bool DeleteProduct(Guid productId);
        List<ProductModel> GetProductSaleBest(string langId, int top);
        List<ProductModel> GetProductPromotion(string langId, int top);
    }
}
