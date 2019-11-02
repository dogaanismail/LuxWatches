using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;

namespace MvcApplication1.DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal: EfEntityRepositoryBase<Brands,luxwatch_databaseEntities>, IBrandDal
    {
    }
}
