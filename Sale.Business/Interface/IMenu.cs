using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface IMenu : IRepository<Menu>
    {
        
        DataBackEndRespone GetMenu(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool Save(About about, AboutLang aboutLg);
        bool Delete(Guid aboutId);

        #region Menu
        List<MenuModel> GetMenuData(string langId);
        List<SubMenu> GetMenuProduct(string langId);
        #endregion EndMenu
    }
}
