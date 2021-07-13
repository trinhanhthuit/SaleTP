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
    public class TextDataBO : Repository<TextData>, ITextData
    {
        public List<TextData> GetData(string langId, string code)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("LangID", langId);
                    param.Add("PageCode", "%"+code+"%");
                    string sqlQuery = @"SELECT * FROM dbo.TextData WHERE LangID=@LangID AND PageCode like @PageCode";
                    return db.Database.Connection.Query<TextData>(sqlQuery, param).ToList();
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
