using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IAbout : IRepository<About>
    {
        AboutModel GetData(string langId);
        DataBackEndRespone GetAbout(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool SaveAbout(About about, AboutLang aboutLg);
        bool DeleteAbout(Guid aboutId);
    }
}
