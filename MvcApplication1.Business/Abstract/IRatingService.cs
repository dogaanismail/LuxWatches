using MvcApplication1.Entities;

namespace MvcApplication1.Business.Abstract
{
    public interface IRatingService
    {
        void Add(Rating rating);

        void AddRating(int product_id, int userID, int rate);
        int FindUser(int product_id, int userID);
    }
}
