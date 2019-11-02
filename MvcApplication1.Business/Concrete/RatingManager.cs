using MvcApplication1.Business.Abstract;
using MvcApplication1.Business.DependencyResolvers.Ninject;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using System;
using System.Linq;

namespace MvcApplication1.Business.Concrete
{
    public class RatingManager : IRatingService
    {
        private IRatingDal _IRatingDal;
        private IProductService _productservice = InstanceFactory.GetInstance<IProductService>();

        public RatingManager(IRatingDal IRatingDal)
        {
            _IRatingDal = IRatingDal;
        }

        public void Add(Rating rating)
        {
            throw new System.NotImplementedException();
        }

        public void AddRating(int product_id, int userID, int rate)
        {
             Product product = _productservice.GetByProductId(product_id);

            int totalvalue = Convert.ToInt16(product.TotalRating);
            totalvalue += rate;

            int totalraters = Convert.ToInt16(product.TotalRaters);
            totalraters += 1;

            int average = totalvalue / totalraters;

            product.TotalRating = totalvalue;
            product.TotalRaters = totalraters;
            product.AverageRating = average;
            _productservice.Update(product);
           
            _IRatingDal.Add(new Rating
            {
                RatingValue = rate,
                UserID=userID,
                ProductID=product_id,
                CreateDate= DateTime.Now               
            });

           

        }

        public int FindUser(int product_id, int userID)
        {
           return _IRatingDal.Query(x => x.ProductID == product_id && x.UserID == userID).ToList().Count();
        }
    }
}
