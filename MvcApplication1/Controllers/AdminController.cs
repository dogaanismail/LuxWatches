//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Data;
//using MvcApplication1.Models;
//using System.IO;
//using MvcApplication1.ALLProcess;
//namespace MvcApplication1.Controllers
//{
//    public class AdminController : Controller
//    {
//        //
//        // GET: /Admin/
//        Repository<Product> _product = new Repository<Product>();
//        Repository<Categories> _cat = new Repository<Categories>();
//        Repository<Brands> _brand = new Repository<Brands>();
//        Repository<Photos> photo = new Repository<Photos>();

//        public ActionResult Index()
//        {
//            return View();
       
//        }
//        public ActionResult Admin()
//        {//When the page is loaded, the products list are showed.
//            return View(_product.getList());
//        }
//        [HttpGet]
//        public ActionResult ProductCreate()
//        { //There are page load events.

//            SelectList list = new SelectList(_cat.getList(), "category_id", "category_name");
//            ViewData["category"] = list;

//            SelectList list2 = new SelectList(_brand.getList(), "brand_id", "brand_name");
//            ViewData["brand"] = list2;

//            return View();
//        }

//        [HttpPost]
//        public ActionResult ProductCreate(System.Web.HttpPostedFileBase UploadFile, string productName, string productPrice, string productDescribe, string productDiscount, FormCollection collection)
//        {
            
//            SelectList list = new SelectList(_cat.getList(), "category_id", "category_name");
//            ViewData["category"] = list;

//            SelectList list2 = new SelectList(_brand.getList(), "brand_id", "brand_name");
//            ViewData["brand"] = list2;

//            var catID = collection["Category"];
//            var brandID = collection["Brand"];       
              
//            string dosyaYolu = Path.GetFileName(UploadFile.FileName);
//            var yuklemeYeri = Path.Combine(Server.MapPath("~/Content/images"), dosyaYolu);                      
//            UploadFile.SaveAs(yuklemeYeri);   
              
//            Product pd = new Product();
//            pd.product_name = productName;
//            pd.product_price = Convert.ToDecimal(productPrice);
//            pd.product_describe = productDescribe;
//            pd.product_photo = dosyaYolu;
//            pd.product_datetime = System.DateTime.Now;
//            pd.product_discount = Convert.ToInt16(productDiscount);
//            pd.category_id = Convert.ToInt16(catID);
//            pd.brand_id = Convert.ToInt16(brandID);
//            _product.Add(pd);
//            return View();
//        }
//        public ActionResult ProductPhoto(int id)
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult ProductPhoto(IEnumerable<HttpPostedFileBase> ChosenFiles)
//        {            
//            Photos pd = new Photos();
//            int product_id = Convert.ToInt16(Url.RequestContext.RouteData.Values["id"]); //önemli
//            foreach (var item in ChosenFiles)
//            {
//                string photoName = Path.GetFileName(item.FileName);
//                var url = Path.Combine(Server.MapPath("~/Content/images/" + item.FileName));
//                item.SaveAs(url);
//                pd.photo_url = photoName;
//                pd.product_id = product_id;
//                photo.Add(pd);
//            }
//           return View();
//        }

//        public ActionResult ProductDelete(int id)
//        {
//            Product pd = _product.SorguyaGoreListele(x => x.product_id == id).FirstOrDefault();
//            _product.Delete(pd);            
//            return RedirectToAction("Admin", "Admin");
//        }
//        [HttpGet]
//        public ActionResult ProductEdit(int id)
//        {
//            Product pd= new Product();
//            SelectList list = new SelectList(_cat.getList(), "category_id", "category_name");
//            ViewData["category"] = list;

//            SelectList list2 = new SelectList(_brand.getList(), "brand_id", "brand_name");
//            ViewData["brand"] = list2;

//            return View(_product.SorguyaGoreListele(i=> i.product_id==id));
//        }

//        [HttpPost]
//        public ActionResult ProductEdit(int ProductID, string ProductName, decimal ProductPrice, string ProductDescribe, System.Web.HttpPostedFileBase UploadFile, string ProductPhoto, int ProductDiscount, FormCollection collection)
//        {
            
//            int catID = Convert.ToInt16(collection["Category"]);
//            int brandID = Convert.ToInt16(collection["Brand"]);

//            Product prod = _product.SorguyaGoreListele(i => i.product_id == ProductID).FirstOrDefault();
//            prod.product_id = ProductID;
//            prod.product_name = ProductName;
//            prod.product_price = ProductPrice;
//            prod.product_describe = ProductDescribe;
//            prod.product_discount = ProductDiscount;

//            prod.brand_id = brandID;
//            prod.category_id = catID;
//            if (UploadFile != null)
//            {
//                string dosyaYolu = Path.GetFileName(UploadFile.FileName);
//                var yuklemeYeri = Path.Combine(Server.MapPath("~/Content/images"), dosyaYolu);
//                UploadFile.SaveAs(yuklemeYeri);
//                prod.product_photo = dosyaYolu;
//            }
//            else
//            {
//                prod.product_photo = ProductPhoto;
//            }
//            _product.Attach(prod);
//            return RedirectToAction("Admin", "Admin");
//        }
//    }
//}
