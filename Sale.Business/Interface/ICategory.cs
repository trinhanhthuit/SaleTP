using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sale.Data;

namespace Sale.Business
{
    public interface ICategory : IRepository<Category>
    {
        CategoryBackEndRespone GetCategory(string searchString, int isActive, string langId, int pageIndex, int pageSize);
        bool SaveCategory(Category category, CategoryLang categoryLg);
        bool DeleteCategory(int categoryId);
    }
}
