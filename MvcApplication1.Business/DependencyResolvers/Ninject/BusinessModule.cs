using MvcApplication1.Business.Abstract;
using MvcApplication1.Business.Concrete;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.DataAccess.Concrete.EntityFramework;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Business.DependencyResolvers.Ninject
{
    public class BusinessModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<EfProductDal>().InSingletonScope();

            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();

            Bind<IBrandService>().To<BrandManager>().InSingletonScope();
            Bind<IBrandDal>().To<EfBrandDal>().InSingletonScope();

            Bind<IPhotoService>().To<PhotoManager>().InSingletonScope();
            Bind<IPhotoDal>().To<EfPhotoDal>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

            Bind<IRatingService>().To<RatingManager>().InSingletonScope();
            Bind<IRatingDal>().To<EfRatingDal>().InSingletonScope();
         
        }
    }
}
