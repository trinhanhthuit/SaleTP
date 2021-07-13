using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface ITextData : IRepository<TextData>
    {
        List<TextData> GetData(string langId, string code);
    }
}
