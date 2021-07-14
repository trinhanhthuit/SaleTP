using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity.SqlServer;
using Sale.Data;
using log4net;

namespace Sale.Business
{
    public class ProductBO : Repository<Product>, IProduct
    {
        #region FrontEnd

        public List<ProductModel> GetData(string langId, string cateid)
        {
            try
            {
                string sqlWhere = "";
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    if (!string.IsNullOrEmpty(cateid))
                    {
                        var cate = db.Categories.Where(m => m.CategoryCode == cateid).FirstOrDefault();
                        if (cate != null)
                        {
                            sqlWhere += " AND c.ids like @CategoryID ";
                            param.Add("CategoryID", "%" + cate.CategoryID.ToString() + ".%");
                        }
                    }

                    string sqlQuery = @"SELECT p.*,l.ProductName, c.CategoryCode, cl.CategoryName, l.ShortContent, l.LongContent, ct.CategoryCode ParentCode, i.ImagePath   
                                        FROM dbo.Product p
                                        INNER join ProductLang l on p.ProductID = l.ProductID AND l.LangID=@LangID
                                        INNER JOIN Category c on p.CategoryID = c.CategoryID
                                        INNER JOIN CategoryLang cl on c.CategoryID =  cl.CategoryID AND cl.LangID=@LangID
                                        INNER JOIN dbo.Category ct on c.ParentID = ct.CategoryID
                                        INNER JOIN dbo.Image i on p.ProductID = i.PathID AND i.IsDefault=1
                                        WHERE p.IsActive = 1 " + sqlWhere;
                    return db.Database.Connection.Query<ProductModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public List<ProductModel> GetProductSaleBest(string langId, int top)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT TOP " + top.ToString() + @" p.*,l.ProductName, c.CategoryCode,l.ShortContent, l.LongContent, i.ImagePath, ct.CategoryCode ParentCode  
                                        FROM dbo.Product p
                                        INNER join ProductLang l on p.ProductID = l.ProductID AND l.LangID = @LangID
                                        INNER JOIN Category c on p.CategoryID = c.CategoryID
                                        INNER JOIN CategoryLang cl on c.CategoryID = cl.CategoryID AND cl.LangID = @LangID
                                        INNER JOIN dbo.Category ct on c.ParentID = ct.CategoryID
                                        INNER JOIN[Image] i on p.ProductID = i.PathID AND i.IsDefault=1
                                        WHERE p.IsActive = 1
                                        ORDER BY p.SaleQuanlity desc ";
                    return db.Database.Connection.Query<ProductModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public List<ProductModel> GetProductPromotion(string langId, int top)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    param.Add("Top", top.ToString());
                    string sqlQuery = @"SELECT TOP " + top.ToString() + @" p.*,l.ProductName, c.CategoryCode,l.ShortContent, l.LongContent, i.ImagePath, ct.CategoryCode ParentCode  
                                        FROM dbo.Product p
                                        INNER join ProductLang l on p.ProductID = l.ProductID AND l.LangID=@LangID
                                        INNER JOIN Category c on p.CategoryID = c.CategoryID
                                        INNER JOIN CategoryLang cl on c.CategoryID =  cl.CategoryID AND cl.LangID=@LangID
                                        INNER JOIN dbo.Category ct on c.ParentID = ct.CategoryID
                                        INNER JOIN [Image] i on p.ProductID = i.PathID AND i.IsDefault=1
                                        WHERE p.IsActive = 1 AND DiscountPrice<SalePrice
                                        ORDER BY c.CategoryID desc ";
                    return db.Database.Connection.Query<ProductModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public List<CategoryModel> GetCategory(string langId, int parentId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    param.Add("ParentId", parentId);
                    string sqlQuery = @"SELECT c.*, l.CategoryName  FROM dbo.Category c
                                        INNER JOIN  dbo.CategoryLang l on c.CategoryID = l.CategoryID AND l.LangID=@LangID
                                        WHERE IsActive = 1 AND ParentId=@ParentId";
                    return db.Database.Connection.Query<CategoryModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }


        public List<ServiceModel> GetService(string langId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT c.*,l.ServiceName, l.Title, l.ShortContent, l.LongContent, i.ImagePath
                                        FROM [dbo].[Service] c
                                        INNER JOIN  dbo.ServiceLang l on c.ServiceID = l.ServiceID AND l.LangID=@LangID
                                        LEFT JOIN [Image] i on c.ServiceID = i.PathID
                                        WHERE c.IsActive = 1";
                    return db.Database.Connection.Query<ServiceModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public List<Image> GetImage(int categoryId, string pathId)
        {
            try
            {
                string sqlQuerry = "WHERE 1=1 ";
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    if (categoryId != 0)
                    {
                        sqlQuerry += " AND [CategoryID]=@CategoryID ";
                        param.Add("CategoryID", categoryId);
                    }
                    if (!string.IsNullOrEmpty(pathId))
                    {
                        sqlQuerry += " AND PathID=@PathID ";
                        param.Add("PathID", pathId);
                    }
                    string sqlQuery = @"select * from [Image] " + sqlQuerry + " Order by SortOrder ";
                    return db.Database.Connection.Query<Image>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }

        public List<TestimonialModel> GetTestimonial(string langId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT * FROM [dbo].[Testimonial] c
                                        INNER JOIN  dbo.TestimonialLang l on c.TestimonialID = l.TestimonialID AND l.LangID=@LangID
                                        WHERE IsActive = 1";
                    return db.Database.Connection.Query<TestimonialModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }

        public List<CategoryTreeModel> GetCategoryTree(string langId)
        {
            try
            {
                List<CategoryTreeModel> categroyTrees = new List<CategoryTreeModel>();
                foreach (var row in this.GetCategory(langId, 0))
                {
                    var categroyTree = new CategoryTreeModel();
                    categroyTree.CategoryID = row.CategoryID;
                    categroyTree.CategoryCode = row.CategoryCode;
                    categroyTree.CategoryName = row.CategoryName;
                    categroyTree.SubCategories = this.GetCategory(langId, row.CategoryID);
                    categroyTrees.Add(categroyTree);
                }

                return categroyTrees;

            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }

        #endregion

        #region BackEnd
        public ProductBackEndRespone GetProduct(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            try
            {
                var respone = new ProductBackEndRespone();
                var products = new List<ProductModel>();

                using (var db = new DBEntities())
                {
                    string sqlWhere = " WHERE 1=1 ";
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    if (isActive != -1)
                    {
                        sqlWhere += " AND p.IsActive=@IsActive ";
                        param.Add("IsActive", isActive);

                    }
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sqlWhere += " AND dbo.fn_ConvertToUnsign(l.ProductName) Like @SearchString OR dbo.fn_ConvertToUnsign(p.ProductCode) Like @SearchString ";
                        param.Add("SearchString", "%" + searchString + "%");
                    }
                    string sqlQueryCount = @"SELECT Count(1)  FROM dbo.Product p
                                        LEFT JOIN ProductLang l on p.ProductID = l.ProductID AND l.LangID=@LangID
                                        LEFT JOIN Category c on p.CategoryID = c.CategoryID
                                        LEFT JOIN CategoryLang cl on c.CategoryID =  cl.CategoryID AND cl.LangID=@LangID 
                                        LEFT JOIN [Image] i  on p.ProductID = i.PathID AND i.IsDefault=1 " + sqlWhere;
                    respone.TotalRows = db.Database.Connection.Query<int>(sqlQueryCount, param).Single();

                    string sqlQuery = @"SELECT p.*, l.ProductName, c.CategoryCode, cl.CategoryName, l.ShortContent, l.LongContent, l.ProductLangID, IIF(i.ImagePath IS NULL, '', i.ImagePath) ImagePath
                                        FROM dbo.Product p
                                        LEFT JOIN ProductLang l on p.ProductID = l.ProductID AND l.LangID=@LangID
                                        LEFT JOIN Category c on p.CategoryID = c.CategoryID
                                        LEFT JOIN CategoryLang cl on c.CategoryID =  cl.CategoryID AND cl.LangID=@LangID 
                                        LEFT JOIN [Image] i  on p.ProductID = i.PathID AND i.IsDefault=1 " + sqlWhere +
                                        "Order By p.ProductCode OFFSET " + pageIndex * pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";

                    respone.Products = db.Database.Connection.Query<ProductModel>(sqlQuery, param).ToList();

                    return respone;
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public bool CheckProduct(Product product)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    if (product.ProductID == Guid.Empty)
                    {
                        var temp = db.Products.Where(m => m.ProductCode == product.ProductCode).ToList();
                        if (temp.Any())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var temp = db.Products.Where(m => m.ProductCode == product.ProductCode && m.ProductID != product.ProductID).ToList();
                        if (temp.Any())
                        {
                            return false;
                        }
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SaveProduct(Product product, ProductLang productLg, List<Image> images)
        {
            try
            {
                string urlUpload = System.Configuration.ConfigurationManager.AppSettings["UrlUpload"];
                using (var db = new DBEntities())
                {
                    if (product.ProductID == Guid.Empty)
                    {
                        product.ProductID = Guid.NewGuid();
                        db.Products.Add(product);
                        productLg.ProductLangID = Guid.NewGuid();
                        productLg.ProductID = product.ProductID;
                        db.ProductLangs.Add(productLg);
                        foreach (var row in images)
                        {
                            row.ImageID = Guid.NewGuid();
                            row.PathID = product.ProductID;
                            row.ImagePath = urlUpload + row.ImagePath;
                            row.CategoryID = 0;
                            
                            db.Images.Add(row);
                        }
                    }
                    else
                    {
                        db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        var productLang = db.ProductLangs.Where(m => m.ProductID == product.ProductID && m.LangID == productLg.LangID).FirstOrDefault();
                        db.ProductLangs.Remove(productLang);
                        db.SaveChanges();
                        var tempImages = db.Images.Where(m => m.PathID == product.ProductID).ToList();
                        if (tempImages != null && tempImages.Count > 0)
                        {
                            db.Images.RemoveRange(tempImages);
                            db.SaveChanges();
                        }

                        productLg.ProductLangID = Guid.NewGuid();
                        db.ProductLangs.Add(productLg);
                        foreach (var row in images)
                        {
                            row.ImageID = Guid.NewGuid();
                            row.PathID = product.ProductID;
                            row.ImagePath = urlUpload + row.ImagePath.Replace(urlUpload,"");
                            row.CategoryID = 0;
                            db.Images.Add(row);
                        }
                    }

                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool DeleteProduct(Guid productId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var product = db.Products.Where(m => m.ProductID == productId).FirstOrDefault();
                    db.Products.Remove(product);
                    var productLangs = db.ProductLangs.Where(m => m.ProductID == product.ProductID).ToList();
                    db.ProductLangs.RemoveRange(productLangs);
                    var tempImages = db.Images.Where(m => m.PathID == product.ProductID).ToList();
                    if (tempImages != null && tempImages.Count > 0)
                    {
                        db.Images.RemoveRange(tempImages);
                    }
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion 
    }
}
