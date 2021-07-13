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
    public class EmployeeBO : Repository<Employee>, IEmployee
    {
        public List<EmployeelModel> GetData(int top, string langId)
        {
            try
            {
                string sQuerytop = "";
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    if (top != 0)
                    {
                        sQuerytop += "TOP " +top.ToString();
                    }
                    param.Add("LangID", langId);
                    string sqlQuery = @"SELECT "+ sQuerytop + @"* FROM dbo.Employee p
                                        INNER join EmployeeLang l on p.EmployeeID = l.EmployeeID AND l.LangID=@LangID
                                        WHERE IsActive=1
                                        ORDER BY p.SortOrder";
                    return db.Database.Connection.Query<EmployeelModel>(sqlQuery, param).ToList();
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
