using MvcApplication1.Business.Abstract;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using System.Collections.Generic;

namespace MvcApplication1.Business.Concrete
{

    public class CategoryManager: ICategoryService
    {
        private ICategoryDal _categorydal;

        public CategoryManager(ICategoryDal categorydal)
        {
            _categorydal = categorydal;
        }

        public List<Categories> getList()
        {
            return _categorydal.getList();
        }


        public void Add(Categories category)
        {
            _categorydal.Add(category);
        }

        public void Delete(Categories category)
        {
            _categorydal.Delete(category);
        }

        public void Update(Categories category)
        {
            _categorydal.Update(category);
        }

        public List<Categories> include(string include)
        {
           return _categorydal.Include(include);
        }

        public Categories GetByCategory(int? nullable)
        {
            return _categorydal.Find(p => p.category_id == nullable);
        }
    }
}
