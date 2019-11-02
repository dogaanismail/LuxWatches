using MvcApplication1.Business.Abstract;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MvcApplication1.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> getList()
        {
            return _productDal.getList();
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public List<Product> Query2(string _productname)
        {
            return _productDal.Query(i => i.product_name.Contains(_productname));
        }

        public Product GetByProductId(int id)
        {
            return _productDal.Find(p => p.product_id == id);
        }

        public List<Product> Query()
        {
            return _productDal.List(x => x.product_datetime).Take(3).ToList();
        }

        public List<Product> GetByCategory(int id)
        {
            return _productDal.Query(x => x.category_id == id);
        }

        public List<Product> GetByCategoryandBrand(int id, int brand)
        {
            return _productDal.Query(x => x.category_id == id && x.brand_id == brand);
        }

        public List<Product> getByProductList(int id)
        {

            return _productDal.GetAllLazyLoad(x=>x.product_id==id, x=>x.Categories, x=>x.Brands, x=>x.Rating).ToList();
        }
       
        public Product Find(int cat)
        {
            return _productDal.Find(x => x.category_id == cat);
        }

        public List<Product> Filter(int cat,List<int> brand_id, decimal? min, decimal? max)
        {
            if (max ==null && min==null)
            {
                return _productDal.Query(x => x.category_id == cat && brand_id.Any(element => element == x.brand_id));
            }
            else
            {
                return _productDal.Query(x => x.category_id == cat && brand_id.Any(element => element == x.brand_id)).Where(x=>x.product_price>=min && x.product_price<=max).ToList();
            }
            
        }

        public List<Product> Filter(int cat)
        {
            return _productDal.Query(x => x.category_id == cat);
        }

        public List<Product> Filter2(int cat, decimal? min, decimal? max)
        {
            return _productDal.Query(x => (x.category_id == cat) && (x.product_price >= min && x.product_price <= max));
        }
    }
}
