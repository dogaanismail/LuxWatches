using MvcApplication1.Entities;
using System.Collections.Generic;

namespace MvcApplication1.Business.Abstract
{
    public interface ICategoryService
    {
       List<Categories> getList();
       void Add(Categories category);
       void Delete(Categories category);
       void Update(Categories category);

       List<Categories> include(string include);
       Categories GetByCategory(int? nullable);
    }
}
