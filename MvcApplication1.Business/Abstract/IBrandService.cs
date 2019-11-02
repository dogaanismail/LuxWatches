using MvcApplication1.Entities;
using System.Collections.Generic;

namespace MvcApplication1.Business.Abstract
{
    public interface IBrandService
    {
        List<Brands> getList();
        void Add(Brands brands);
        void Delete(Brands brands);
        void Update(Brands brands);
        Brands GetByBrand(int? nullable);

        List<Brands> GetByCategory(int cat);
    }
}
