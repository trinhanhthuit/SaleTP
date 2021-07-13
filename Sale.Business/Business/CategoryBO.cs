using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity.SqlServer;
using Sale.Data;

namespace Sale.Business
{
    public class CategoryBO : Repository<Category>, ICategory
    {
        public CategoryBackEndRespone GetCategory(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            try
            {
                var respone = new CategoryBackEndRespone();

                using (var db = new DBEntities())
                {
                    string sqlWhere = " WHERE 1=1 ";
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    if (isActive != -1)
                    {
                        sqlWhere += " AND c.IsActive=@IsActive ";
                        param.Add("IsActive", isActive);

                    }
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sqlWhere += " AND dbo.fn_ConvertToUnsign(cl.CategoryName) Like @SearchString OR dbo.fn_ConvertToUnsign(c.CategoryCode) Like @SearchString ";
                        param.Add("SearchString", "%" + searchString + "%");
                    }
                    string sqlQueryCount = @"SELECT COUNT(1)
                                            FROM Category c 
                                            INNER JOIN CategoryLang cl on c.CategoryID =  cl.CategoryID AND cl.LangID=@LangID 
                                            LEFT JOIN CategoryLang prl on c.ParentID = prl.CategoryID AND cl.LangID=@LangID " + sqlWhere;
                    respone.TotalRows = db.Database.Connection.Query<int>(sqlQueryCount, param).Single();

                    string sqlQuery = @"SELECT c.*, cl.CategoryName, cl.CategoryLangID, cl.LangID, prl.CategoryName CategoryNameParent 
                                        FROM Category c 
                                        INNER JOIN CategoryLang cl on c.CategoryID = cl.CategoryID AND cl.LangID=@LangID 
                                        LEFT JOIN CategoryLang prl on c.ParentID = prl.CategoryID AND cl.LangID=@LangID " + sqlWhere +
                                        "Order By c.CategoryCode OFFSET " + pageIndex + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";

                    respone.Categories = db.Database.Connection.Query<CategoryModel>(sqlQuery, param).ToList();

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

        public bool SaveCategory(Category category, CategoryLang categoryLg)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    if (category.CategoryID == 0)
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();
                        categoryLg.CategoryID = category.CategoryID;
                        db.CategoryLangs.Add(categoryLg);
                    }
                    else
                    {
                        db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                        var cateLg = db.CategoryLangs.Where(m => m.CategoryID == category.CategoryID && m.LangID == categoryLg.LangID).FirstOrDefault();
                        db.CategoryLangs.Remove(cateLg);
                        db.SaveChanges();
                        db.CategoryLangs.Add(categoryLg);
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
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var category = db.Categories.Where(m => m.CategoryID == categoryId).FirstOrDefault();
                    db.Categories.Remove(category);
                    var cateLangs = db.CategoryLangs.Where(m => m.CategoryID == category.CategoryID).ToList();
                    db.CategoryLangs.RemoveRange(cateLangs);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
