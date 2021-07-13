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
    public class AboutBO : Repository<About>, IAbout
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AboutModel GetData(string langId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT * FROM dbo.About c
                                        INNER JOIN  dbo.AboutLang l on c.AboutID = l.AboutID AND l.LangID=@LangID
                                        WHERE IsActive = 1";
                    return db.Database.Connection.Query<AboutModel>(sqlQuery, param).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }
        public DataBackEndRespone GetAbout(string searchString, int isActive, string langId, int pageIndex, int pageSize)
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
                        sqlWhere += " AND c.IsActive=@IsActive ";
                        param.Add("IsActive", isActive);
                    }
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sqlWhere += " AND dbo.fn_ConvertToUnsign(l.AboutName) Like @SearchString ";
                        param.Add("SearchString", "%" + searchString + "%");
                    }
                    string sqlQueryCount = @"SELECT COUNT(1) FROM dbo.About c
                                        INNER JOIN  dbo.AboutLang l on c.AboutID = l.AboutID AND l.LangID=@LangID " + sqlWhere;
                    respone.TotalRows = db.Database.Connection.Query<int>(sqlQueryCount, param).Single();

                    string sqlQuery = @"SELECT * FROM dbo.About c
                                        INNER JOIN  dbo.AboutLang l on c.AboutID = l.AboutID AND l.LangID=@LangID " + sqlWhere +
                                        "Order By l.AboutTitle OFFSET " + pageIndex + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";

                    respone.Results = db.Database.Connection.Query<AboutModel>(sqlQuery, param).ToList();
                    return respone;
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                log.Error(ex);
                return null;
            }
        }

        public bool SaveAbout(About about, AboutLang aboutLg)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    if (about.AboutID == Guid.Empty)
                    {
                        about.AboutID = Guid.NewGuid();
                        db.Abouts.Add(about);
                        db.SaveChanges();
                        aboutLg.AboutLangID = Guid.NewGuid();
                        aboutLg.AboutID = about.AboutID;
                        db.AboutLangs.Add(aboutLg);
                    }
                    else
                    {
                        db.Entry(about).State = System.Data.Entity.EntityState.Modified;
                        var aboutLang = db.AboutLangs.Where(m => m.AboutID == about.AboutID && m.LangID == aboutLg.LangID).FirstOrDefault();
                        db.AboutLangs.Remove(aboutLang);
                        db.SaveChanges();
                        db.AboutLangs.Add(aboutLg);
                    }

                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }
        public bool DeleteAbout(Guid aboutId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var about = db.Abouts.Where(m => m.AboutID == aboutId).FirstOrDefault();
                    db.Abouts.Remove(about);
                    var aboutLangs = db.AboutLangs.Where(m => m.AboutID == about.AboutID).ToList();
                    db.AboutLangs.RemoveRange(aboutLangs);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }
    }
}
