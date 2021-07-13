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
    public class WhyUSBO : Repository<WhyU>, IWhyUS
    {
        public WhyUSModel GetData(string langId)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT * FROM dbo.WhyUS p
                                        INNER join WhyUSLang l on p.ID = l.ID AND l.LangID=@LangID
                                        WHERE IsActive=1
                                        ORDER BY p.SortOrder";
                    return db.Database.Connection.Query<WhyUSModel>(sqlQuery, param).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }

        public List<WhyUSDetailModel> GetDataDetail(int top,string langId)
        {
            try
            {
                string sQuerytop = "";
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    if (top != 0)
                    {
                        sQuerytop += "TOP " + top.ToString();
                    }
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT " + sQuerytop + @"* FROM dbo.WhyUSDetail p
                                        INNER join WhyUSDetailLang l on p.WhyUSDetailID = l.WhyUSDetailID AND l.LangID=@LangID
                                        WHERE IsActive=1
                                        ORDER BY p.SortOrder";
                    return db.Database.Connection.Query<WhyUSDetailModel>(sqlQuery, param).ToList();
                }
            }
            catch (Exception ex)
            {
                this.AddMessage(MessageError.MSGCODE_000001, ex.Message);
                Logger.Error(GetType(), ex);
                return null;
            }
        }

    }
}
