using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using MvcApplication1.Business.Abstract;
using MvcApplication1.Entities;
using MvcApplication1.Business.DependencyResolvers.Ninject;
using MvcApplication1.Entities.ViewModels;
using MvcApplication1.Entities.Messages;
using MvcApplication1.Business.BusinessResult;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productservice = InstanceFactory.GetInstance<IProductService>();
        private ICategoryService _categoryservice = InstanceFactory.GetInstance<ICategoryService>();
        private IUserService _userservice = InstanceFactory.GetInstance<IUserService>();
        public ActionResult Index()
        {
            ViewBag.product = _productservice.getList().OrderByDescending(x => x.product_id);
            return View();                
        }
        public ActionResult ShoppingCart()
        {
            return View();
        }       
        public ActionResult Category()
        {
            ViewBag.category = _categoryservice.include("Brands");
            return PartialView();
        }
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string productName)
        {
            ViewBag.product = _productservice.Query2(productName);
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessResults<Users> user = _userservice.Add(model);

                if (user.Errors.Count > 0)
                {

                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                return RedirectToAction("RegisterOk");
            }
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessResults<Users> user = _userservice.Login(model);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError(" ", x.Message));
                    return View(model);
                }
                Session["login"] = user.result;
                return RedirectToAction("Index");
                
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            Session.Remove("login");
            return RedirectToAction("Index");
        }

        public ActionResult UserActivate(Guid id)
        {
            BusinessResults<Users> user = _userservice.ActivateUser(id);
            if (user.Errors.Count > 0)
            {
                TempData["errors"] = user.Errors;
                return RedirectToAction("UserActivationCancel");
            }
            return RedirectToAction("UserActivationOk");
        }

        public ActionResult UserActivationOk()
        {
            return View();
        }
        public ActionResult UserActivationCancel()
        {
            List<ErrorMessageObj> errors = null;

            if (TempData["errors"] !=null)
            {
               errors = TempData["errors"] as List<ErrorMessageObj>;
            }
            return View(errors);
        }

        public  ActionResult Profile()
        {
            Users user = Session["login"] as Users;
            BusinessResults<Users> ShowUser = _userservice.GetByUserID(user.UserID);
            if (ShowUser.Errors.Count > 0)
            {
                //Hata Ekranı
            }
            return View(ShowUser.result);
        }
        public PartialViewResult EditProfile()
        {
            Users user = Session["login"] as Users;
            BusinessResults<Users> ShowUser = _userservice.GetByUserID(user.UserID);
            if (ShowUser.Errors.Count > 0)
            {
                //Hata Ekranı
            }
            return PartialView(ShowUser.result);
        }

        [HttpPost]
        public string UpdateProfile(Users EditUser)
        {
            BusinessResults<Users> result = _userservice.UpdateProfile(EditUser);
            if (result.Errors.Count > 0)
            {
                return "Update is not successful";
            }
            Session["login"] = result.result;
            return "Update is successful";

        }
        public PartialViewResult UserAddress()
        {
 
            return PartialView();
            
        }
        
    }
}
