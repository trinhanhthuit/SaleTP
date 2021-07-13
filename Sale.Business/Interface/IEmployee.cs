using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IEmployee : IRepository<Employee>
    {
        List<EmployeelModel> GetData(int top, string langId);
    }
}
