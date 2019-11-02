using MvcApplication1.Business.Abstract;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using System;
using System.Collections.Generic;

namespace MvcApplication1.Business.Concrete
{
    public class BrandManager: IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public List<Brands> getList()
        {
            return _brandDal.getList();
        }

        public void Add(Brands brands)
        {
            throw new NotImplementedException();
        }

        public void Delete(Brands brands)
        {
            throw new NotImplementedException();
        }

        public void Update(Brands brands)
        {
            throw new NotImplementedException();
        }


        public Brands GetByBrand(int? nullable)
        {
            return _brandDal.Find(p => p.brand_id == nullable);
        }


        public List<Brands> GetByCategory(int cat)
        {
            return _brandDal.Query(x => x.category_id == cat);
        }
    }
}
