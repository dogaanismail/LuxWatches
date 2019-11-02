using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;

namespace MvcApplication1.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal: EfEntityRepositoryBase<Users, luxwatch_databaseEntities>, IUserDal
    {
    }
}
