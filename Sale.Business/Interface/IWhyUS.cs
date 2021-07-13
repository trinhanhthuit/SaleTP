using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IWhyUS : IRepository<WhyU>
    {
        WhyUSModel GetData(string langId);
        List<WhyUSDetailModel> GetDataDetail(int top, string langId);
    }
}
