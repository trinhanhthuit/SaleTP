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
    public class UserBO : Repository<User>, IUser
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public User CheckLogin(string userName, string passWord)
        {
            try
            {
                using (var db = new DBEntities())
                {
                    var select = this.GetList(m => m.UserName.ToLower() == userName.ToLower() || m.Email == userName.ToLower());
                    if (!select.Any())
                    {
                        Logger.Error("User name " + userName + " doesn't existing");
                        return null;
                    }
                    var user = select.First();
                    if (user.IsActive == false)
                    {
                        Logger.Error("User name " + userName + " is not active");
                        return null;
                    }

                    if (user.Password.ToLower().Contains(passWord.ToLower()))
                    {
                        //Login OK & get informatio
                        return user;
                    }

                    return null;
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
