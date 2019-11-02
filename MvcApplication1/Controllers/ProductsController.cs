using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using MvcApplication1.Business.Abstract;
using MvcApplication1.Business.DependencyResolvers.Ninject;
using System.Web.Script.Services;
using System.Web.Services;
using MvcApplication1.Entities;

namespace MvcApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService _productservice = InstanceFactory.GetInstance<IProductService>();
        private ICategoryService _categoryservice = InstanceFactory.GetInstance<ICategoryService>();
        private IBrandService _brandservice = InstanceFactory.GetInstance<IBrandService>();
        private IPhotoService _photoservice = InstanceFactory.GetInstance<IPhotoService>();
        private IRatingService _ratingservice = InstanceFactory.GetInstance<IRatingService>();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            luxwatch_databaseEntities db = new luxwatch_databaseEntities();
      
            ViewBag.photos = _photoservice.GetByPhoto(id);
            ViewBag.product = _productservice.getByProductList(id);
            return View();
        }
        public ActionResult LatestProduct()
        {
            ViewBag.LatestProduct = _productservice.Query();
            return PartialView();
        }

        public ActionResult CategoryProducts()
        {
            int cat = Convert.ToInt16(Request.QueryString["cat"]);
            int brand = Convert.ToInt16(Request.QueryString["brand"]);

            ViewBag.category = _categoryservice.include("Brands").Where(x=>x.category_id==cat);
            ViewBag.brand = _brandservice.getList();

            if (brand == 0)
            {
                var product = _productservice.GetByCategory(cat).ToList();
                return View(product.ToList());
            }

            else
            {
               var product= _productservice.GetByCategoryandBrand(cat, brand);              
                return View(product.ToList());
            }
          
        }

        [HttpPost]
        public PartialViewResult GetProduct(int cat,List<int> brand_id,decimal? min, decimal? max)
        {
            //Session["brand_id"] = brand_id; //hatalı
            //if (delete_brand != null)
            //{
            //    brand_id.RemoveAll(x => x == delete_brand);
            //    Session["brand_id"] = brand_id;
            //}
            ViewBag.catlist = _categoryservice.getList(); 
            Session["cat"] = cat;

            if (brand_id == null)
            {
                var product = _productservice.Filter2(cat,min,max).ToList();      
                return PartialView("getProduct",product.ToList());
            }

            else
            {
                var product= _productservice.Filter(cat,brand_id,min,max).ToList();
                return PartialView("getProduct",product.ToList());
            } 

        }

        [HttpPost]
        public ActionResult AddCart(int? id, string number)
        {

            string quantity2 = number.ToString();
            if (Session["cart"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("product_id", typeof(int));
                dt.Columns.Add("quantity", typeof(int));
                dt.Columns.Add("price", typeof(decimal));
                dt.Columns.Add("product_discount", typeof(int));
                dt.Columns.Add("totalprice", typeof(decimal));
                dt.Columns.Add("product_photo", typeof(string));

                dt.Columns["id"].AutoIncrement = true;
                dt.Columns["id"].AutoIncrementSeed = 1;
                dt.Columns["id"].AutoIncrementStep = 1;
                dt.PrimaryKey = new DataColumn[] { dt.Columns["id"] };
                Session["cart"] = dt;
            }
            bool varmi = Kontrol(A); //important

            if (varmi == false)
            {
                DataTable dt2 = new DataTable();
                dt2 = (DataTable)Session["cart"];
                DataRow column2 = dt2.NewRow();
                column2["product_id"] = id;
                column2["quantity"] = quantity2;
                var prop = _productservice.GetByProductId( Convert.ToInt16(id));

                decimal price = Convert.ToDecimal(prop.product_price);
                decimal price2 = Convert.ToDecimal(price) * Convert.ToDecimal(quantity2);
                decimal totalprice = Convert.ToDecimal((price2 * prop.product_discount) / 100);
                decimal totalprice2 = price2 - totalprice;

                string photo = prop.product_photo;
                column2["product_discount"] = prop.product_discount;
                column2["price"] = price;
                column2["totalprice"] = totalprice2;
                column2["product_photo"] = photo;
                dt2.Rows.Add(column2);
                Session["cart"] = dt2;
            }

            else if (varmi == true)
            {
                Artir(A);
            }

            return RedirectToAction("Detail", "Products", routeValues: new { id });
        }

        public ActionResult DeleteProduct(int? id)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Session["cart"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt16(dt.Rows[i]["product_id"]) == id)
                {
                    dt.Rows[i].Delete();
                    HttpContext.Session["cart"] = dt;

                    if (dt.Rows.Count == 0)
                    {
                        Session.Remove("cart");
                    }
                    break;
                }

            }

            return RedirectToAction("ShoppingCart", "Home");
        }
        private bool Kontrol(string a)
        {
            int proId = Convert.ToInt16(Url.RequestContext.RouteData.Values["id"]);
            bool sonuc = false;

            DataTable dt = new DataTable();
            dt = (DataTable)Session["cart"];
            if (Session["cart"] != null)
            {
                var sorgu = (from row in dt.AsEnumerable()
                             where row.Field<int>("product_id") == proId
                             select row);
                if (sorgu.Count() != 0)
                {
                    sonuc = true;
                }
            }

            return sonuc;
        }
        private void Artir(string a)
        {
            string quantity2 = Request.Form["number"];
            int proId = Convert.ToInt16(Url.RequestContext.RouteData.Values["id"]);
            DataTable dt2 = new DataTable();
            dt2 = (DataTable)Session["cart"];

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                if (Convert.ToInt16(dt2.Rows[i]["product_id"]) == proId)
                {

                    int quantity = Convert.ToInt16(dt2.Rows[i]["quantity"]);
                    int discount = Convert.ToInt16(dt2.Rows[i]["product_discount"]);
                    decimal totalprice3 = Convert.ToDecimal(dt2.Rows[i]["totalprice"]);


                    quantity = quantity + Convert.ToInt16(quantity2);
                    dt2.Rows[i]["quantity"] = quantity;

                    decimal price = Convert.ToDecimal(dt2.Rows[i]["price"]);
                    decimal price2 = Convert.ToDecimal(price) * Convert.ToDecimal(quantity2);
                    decimal totalprice = Convert.ToDecimal((price2 * discount) / 100);
                    decimal totalprice2 = price2 - totalprice;
                    totalprice3 = totalprice3 + totalprice2;

                    dt2.Rows[i]["totalprice"] = totalprice3;
                    Session["cart"] = dt2;

                }
            }
        }

        public string A { get; set; }

       [HttpPost]
        public int SaveRating(int product_id, int rate)
        {
            if (!(Session["login"] is Users user))
            {
                //Hata ekranı
                return 0;
            }
            else
            {
                int count = _ratingservice.FindUser(product_id, user.UserID);
                if (count==0)
                {
                    _ratingservice.AddRating(product_id, user.UserID, rate);
                    Product product = _productservice.GetByProductId(product_id);
                    return Convert.ToInt32(product.AverageRating);
                }
                else
                {
                    //Hata Ekranı
                    return 0;
                }

            }

        }

        }
}

