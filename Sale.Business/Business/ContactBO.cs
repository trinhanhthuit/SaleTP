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
    public class ContactBO : Repository<Contact>, IContact
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataBackEndRespone GetData(string searchString, string fromDate, string toDate, int status, int pageIndex, int pageSize)
        {
            try
            {
                var respone = new DataBackEndRespone();
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    string sqlWhere = " WHERE 1=1 ";
                    if (!string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
                    {
                        if (!string.IsNullOrEmpty(fromDate))
                        {
                            sqlWhere += " AND CAST(CreatedDate AS Date) >= @FromDate ";
                            param.Add("FromDate", fromDate);

                        }
                        if (!string.IsNullOrEmpty(toDate))
                        {
                            sqlWhere += " AND CAST(CreatedDate AS Date) <= @ToDate";
                            param.Add("ToDate", toDate);
                        }
                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        sqlWhere += " AND SearchString Like @SearchString ";
                        param.Add("SearchString", "%" + searchString + "%");

                    }

                    if (status != -1)
                    {
                        sqlWhere += " AND Status = @Status ";
                        param.Add("Status", status);
                    }
                    string sqlQueryCount = @"SELECT COUNT(1) FROM dbo.Contact c " + sqlWhere;
                    respone.TotalRows = db.Database.Connection.Query<int>(sqlQueryCount, param).Single();

                    string sqlQuery = @"SELECT * FROM dbo.Contact c " + sqlWhere +
                        " Order By c.CreatedDate DESC OFFSET " + pageIndex * pageSize + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
                    respone.Results = db.Database.Connection.Query<Contact>(sqlQuery, param).ToList();
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
        public bool Save(Contact contact)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    if (contact.ContactID == 0)
                    {
                        db.Contacts.Add(contact);
                    }
                    else
                    {
                        db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
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

    }
}
