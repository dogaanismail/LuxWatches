using MvcApplication1.Entities;
using System.Collections.Generic;

namespace MvcApplication1.Business.Abstract
{
    public interface IProductService
    {
        List<Product> getList();
        List<Product> Query();

        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);

        List<Product> Query2(string productname);
        Product GetByProductId(int id);
        List<Product> GetByCategory(int id);
        List<Product> GetByCategoryandBrand(int id, int brand);

        List<Product> getByProductList(int id);


        List<Product> Filter(int cat,List<int> brand_id, decimal? min, decimal? max);
        List<Product> Filter(int cat);
        Product Find(int cat);


        List<Product> Filter2(int cat, decimal? min, decimal? max);
    }
}
