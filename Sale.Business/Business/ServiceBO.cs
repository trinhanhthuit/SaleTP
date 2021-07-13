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
    public class ServiceBO : Repository<Service>, IService
    {

        #region Front End
        public List<ServiceModel> GetService(string langId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT * FROM [dbo].[Service] c
                                        INNER JOIN  dbo.ServiceLang l on c.ServiceID = l.ServiceID AND l.LangID=@LangID
                                        WHERE IsActive = 1";
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
        #endregion

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

        public DataBackEndRespone GetService(string searchString, int isActive, string langId, int pageIndex, int pageSize)
        {
            try
            {
                var respone = new DataBackEndRespone();
                var products = new List<ProductModel>();

                using (var db = new DBEntities())
                {
                    string sqlWhere = " WHERE 1=1 ";
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    if (isActive != -1)
                    {
                        sqlWhere += " AND s.IsActive=@IsActive ";
                        param.Add("IsActive", isActive);
                    }
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sqlWhere += " AND dbo.fn_ConvertToUnsign(l.ServiceName) Like @SearchString OR dbo.fn_ConvertToUnsign(s.LinkCode) Like @SearchString ";
                        param.Add("SearchString", "%" + searchString + "%");
                    }
                    string sqlQueryCount = @"SELECT COUNT(1)
                                        FROM [dbo].[Service] s
                                        LEFT JOIN [dbo].[ServiceLang] l on s.ServiceID=l.ServiceID AND l.LangID=@LangID " + sqlWhere;
                    respone.TotalRows = db.Database.Connection.Query<int>(sqlQueryCount, param).Single();

                    string sqlQuery = @"SELECT s.*,l.ServiceLangID, l.ServiceName, l.Title, l.ShortContent, l.LongContent 
                                        FROM [dbo].[Service] s
                                        LEFT JOIN [dbo].[ServiceLang] l on s.ServiceID=l.ServiceID AND l.LangID=@LangID " + sqlWhere +
                                        "Order By l.ServiceName OFFSET " + pageIndex + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";

                    respone.Results = db.Database.Connection.Query<ServiceModel>(sqlQuery, param).ToList();

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
        public bool CheckService(Service service)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    if (service.ServiceID == Guid.Empty)
                    {
                        var temp = db.Services.Where(m => m.LinkCode == service.LinkCode).ToList();
                        if (temp.Any())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var temp = db.Services.Where(m => m.LinkCode == service.LinkCode && m.ServiceID != service.ServiceID).ToList();
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
        public bool SaveService(Service service, ServiceLang serviceLang, List<Image> images)
        {
            var msg = new MessageModel();
            try
            {
                //string urlUpload = System.Configuration.ConfigurationManager.AppSettings["UrlUpload"];
                using (var db = new DBEntities())
                {
                    if (!this.CheckService(service))
                    {
                        return false;
                        msg.Message = "Tồn tại link code.";
                    }
                    if (service.ServiceID == Guid.Empty)
                    {
                        service.ServiceID = Guid.NewGuid();
                        db.Services.Add(service);
                        serviceLang.ServiceLangID = Guid.NewGuid();
                        serviceLang.ServiceID = service.ServiceID;
                        db.ServiceLangs.Add(serviceLang);
                        foreach (var row in images)
                        {
                            row.ImageID = Guid.NewGuid();
                            row.PathID = service.ServiceID;
                            row.ImagePath = row.ImagePath;
                            db.Images.Add(row);
                        }
                    }
                    else
                    {
                        db.Entry(service).State = System.Data.Entity.EntityState.Modified;
                        var servicelg = db.ServiceLangs.Where(m => m.ServiceID == service.ServiceID && m.LangID == serviceLang.LangID).FirstOrDefault();
                        if (servicelg != null)
                        {
                            db.ServiceLangs.Remove(servicelg);
                            db.SaveChanges();
                        }
                        var tempImages = db.Images.Where(m => m.PathID == service.ServiceID).ToList();
                        if (tempImages != null && tempImages.Count > 0)
                        {
                            db.Images.RemoveRange(tempImages);
                            db.SaveChanges();
                        }

                        serviceLang.ServiceLangID = Guid.NewGuid();
                        db.ServiceLangs.Add(serviceLang);
                        foreach (var row in images)
                        {
                            row.ImageID = Guid.NewGuid();
                            row.PathID = service.ServiceID;
                            row.ImagePath = row.ImagePath;
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
        public bool DeleteService(Guid serviceId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var service = db.Services.Where(m => m.ServiceID == serviceId).FirstOrDefault();
                    db.Services.Remove(service);
                    var serviceLangs = db.ServiceLangs.Where(m => m.ServiceID == service.ServiceID).ToList();
                    db.ServiceLangs.RemoveRange(serviceLangs);
                    var tempImages = db.Images.Where(m => m.PathID == service.ServiceID).ToList();
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
    }
}
