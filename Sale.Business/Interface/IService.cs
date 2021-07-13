using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IService: IRepository<Service>
    {
        DataBackEndRespone GetService(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool SaveService(Service service, ServiceLang serviceLang, List<Image> images);
        bool DeleteService(Guid serviceId);
    }
}
