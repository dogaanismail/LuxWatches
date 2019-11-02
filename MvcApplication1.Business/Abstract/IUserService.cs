using MvcApplication1.Business.BusinessResult;
using MvcApplication1.Entities;
using MvcApplication1.Entities.ViewModels;
using System;

namespace MvcApplication1.Business.Abstract
{
    public interface IUserService
    {
        BusinessResults<Users> Add(RegisterViewModel data);
        BusinessResults<Users> Login(LoginViewModel data);
        BusinessResults<Users> ActivateUser(Guid id);

        void Delete(Users user);
        BusinessResults<Users> UpdateProfile(Users user);
        BusinessResults<Users> GetByUserID(int userID);
    }
}
