using MvcApplication1.Business.Abstract;
using MvcApplication1.Business.BusinessResult;
using MvcApplication1.DataAccess.Abstract;
using MvcApplication1.Entities;
using MvcApplication1.Entities.ViewModels;
using MvcApplication1.Entities.Messages;
using System;
using MvcApplication1.Common.Helpers;

namespace MvcApplication1.Business.Concrete
{
    public class UserManager: IUserService
    {
        private IUserDal _userdal;

        public UserManager(IUserDal userdal)
        {
            _userdal = userdal;
        }

      
        public void Delete(Users user)
        {
            throw new NotImplementedException();
        }

        public BusinessResults<Users> Add(RegisterViewModel data) //Adding a user
        {
            Users user = _userdal.Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessResults<Users> IsUser = new BusinessResults<Users>();

            if (user !=null)
            {
                if (user.UserName==data.UserName)
                {
                    IsUser.AddError(ErrorMessageCode.UserNameAlreadyExists, "Username is already exists");
                }
                if (user.Email==data.Email)
                {
                    IsUser.AddError(ErrorMessageCode.EmailAlreadyExists, "Email is already exists");
                }
            }
            else
            {

           int DbResult= _userdal.Add2(new Users()  //Add2 is used because of getting int result.
                {
                    Name=data.Name,
                    SurName=data.SurName,
                    UserName=data.UserName,
                    Email= data.Email,
                    Password= data.Password,
                    ActivateGuid= Guid.NewGuid(),  //Activation Code for Email.
                    IsActive=false,
                    IsAdmin=false,
                });

           if (DbResult >0)
           {
               IsUser.result = _userdal.Find(x => x.Email == data.Email && x.UserName == data.UserName); 
               string siteUri= ConfigHelper.Get<string>("SiteRootUri");
                    string link = $"{siteUri}/Home/UserActivate/{IsUser.result.ActivateGuid}";
                    string body = $"Hello {IsUser.result.UserName};<br><br> <a href='{link}' target='_blank'> Click </a> to activate your account ! .";
               MailHelper.SendMail(body,IsUser.result.Email,"Activating account",true);
           }

            }//end of the else

            return IsUser;
        }

        public BusinessResults<Users> Login(LoginViewModel data) //Login
        {
            BusinessResults<Users> IsUser = new BusinessResults<Users>()
            {
                result = _userdal.Find(x => x.UserName == data.UserName && x.Password == data.Password)
            };
            if (IsUser.result !=null)
            {
                if (IsUser.result.IsActive==false)
                {
                    IsUser.AddError(ErrorMessageCode.UserIsNotActive, "User is not activated");
                    IsUser.AddError(ErrorMessageCode.CheckYourEmail, "Check your Email");
                }
            }
            else
            {
                IsUser.AddError(ErrorMessageCode.UsernameOrPassWrong, "Password or Username is wrong");
            }

            return IsUser;
           
        }
        public BusinessResults<Users> ActivateUser(Guid id)
        {
            BusinessResults<Users> IsUser = new BusinessResults<Users>()
            {
                result = _userdal.Find(x => x.ActivateGuid == id)
            };
            if (IsUser.result !=null)
            {
                if (IsUser.result.IsActive==true)
                {
                    IsUser.AddError(ErrorMessageCode.UserAlreadyActive, "This is already activated");
                    return IsUser;
                }
                IsUser.result.IsActive = true;
                _userdal.Update(IsUser.result);
            }
            else
            {
                IsUser.AddError(ErrorMessageCode.ActivateDoesNotExists, "User cannot be found!");
            }
            return IsUser;
        }

        public BusinessResults<Users> GetByUserID(int userID)
        {
            BusinessResults<Users> user = new BusinessResults<Users>
            {
                result = _userdal.Find(x => x.UserID == userID)
            };

            if (user.result==null)
            {
                user.AddError(ErrorMessageCode.UserIsNotFound, "User is not found!");
            }
            return user;
        }

        public BusinessResults<Users> UpdateProfile(Users user)
        {
            Users FindUser = _userdal.Find(x => x.UserName == user.UserName || x.Email == user.Email);
            BusinessResults<Users> res = new BusinessResults<Users>();
            if (FindUser !=null && FindUser.UserID != user.UserID)
            {
                if (FindUser.UserName== user.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExists, "User Name is Already Exists");
                }
                if (FindUser.Email==user.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, " Email is Already Exists");
                }
                return res;
            }
            res.result = _userdal.Find(x => x.UserID == user.UserID);
            res.result.Name = user.Name;
            res.result.SurName = user.SurName;
            res.result.Password = user.Password;
            res.result.Email = user.Email;
            _userdal.Update(res.result);
            return res;
        }
    }
}
