using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IOrder : IRepository<Order>
    {
        AboutModel GetData(string langId);
        DataBackEndRespone GetOrder(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool SaveOrder(Order order, List<OrderDetail> orderDetails, Customer cus);
        bool DeleteOrder(long id);
    }
}
