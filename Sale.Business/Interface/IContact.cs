using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IContact : IRepository<Contact>
    {
        DataBackEndRespone GetData(string searchString, string fromDate, string toDate, int status, int pageIndex, int pageSize);
        bool Save(Contact contact);
    }
}
