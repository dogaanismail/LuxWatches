using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;

namespace MvcApplication1.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Categories, luxwatch_databaseEntities>, ICategoryDal  //two inherit
    {
        
    }
}
