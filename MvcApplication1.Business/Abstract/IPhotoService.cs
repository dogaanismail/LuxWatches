using MvcApplication1.Entities;
using System.Collections.Generic;

namespace MvcApplication1.Business.Abstract
{
    public interface IPhotoService
    {
        List<Photos> getList();

        void Add(Photos photos);
        void Delete(Photos photos);
        void Update(Photos photos);

        List<Photos> GetByPhotoId(int id);


        List<Photos> GetByPhoto(int id);
    }
}
