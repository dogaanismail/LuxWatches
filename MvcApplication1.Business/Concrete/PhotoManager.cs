using MvcApplication1.Business.Abstract;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using System;
using System.Collections.Generic;

namespace MvcApplication1.Business.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private IPhotoDal _IPhotoDal;

        public PhotoManager(IPhotoDal IPhotoDal)
        {
            _IPhotoDal = IPhotoDal;
        }

        public List<Photos> getList()
        {
            throw new NotImplementedException();
        }

        public void Add(Photos photos)
        {
            throw new NotImplementedException();
        }

        public void Delete(Photos photos)
        {
            throw new NotImplementedException();
        }

        public void Update(Photos photos)
        {
            throw new NotImplementedException();
        }

        public List<Photos> GetByPhotoId(int id)
        {
            return _IPhotoDal.List(p => p.product_id==id);
        }


        public List< Photos> GetByPhoto(int id)
        {
            return _IPhotoDal.Query(x => x.product_id == id);
        }
    }
}
