using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cart2_test.Controllers
{
    public class ProductController : Controller
    {
        private Models. CartContainer db = new Models.CartContainer();

        // GET: Product
        public ActionResult Index()
        {
            List<Models.Product> result = new List<Models.Product>();

            ViewBag.ResultMessage = TempData["ResultMessage"];

            using (Models.CartContainer db = new Models.CartContainer())
            {
                result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Product postback)
        {
            if (this.ModelState.IsValid)
            {
                db.Products.Add(postback);

                db.SaveChanges();

                TempData["ResultMessage"] = $"商品{postback.Name}成功建立";

                return RedirectToAction("Index");
            }

            ViewBag.ResultMessage = "資料有誤，請檢查";

            return View(postback);
        }

        public ActionResult Edit(int id)
        {

            var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
            if (result != default(Models.Product))
            {
                return View(result);
            }
            else
            {
                TempData["resultMessage"] = "資料有誤，請重新操作";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(Models.Product postback)
        {
            var result = db.Products.Find(postback.Id);
            //var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();

            result.Name = postback.Name;
            result.Price = postback.Price;
            result.ImageUrl = postback.ImageUrl;

            db.SaveChanges();

            TempData["ResultMessage"] = $"商品{postback.Name}成功編輯";

            return RedirectToAction("Index");

            // [ValidateAntiForgeryToken]

            /*if (this.ModelState.IsValid)
            {
                var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();

                result.Name = postback.Name;
                result.Price = postback.Price;

                db.SaveChanges();

                TempData["ResultMessage"] = $"商品{postback.Name}成功編輯";

                return RedirectToAction("Index");

            }
            else
            {
                return View(postback);
            }*/
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DelectConfirm(int id)
        {
            var result = db.Products.Find(id);
            if (result != null)
            {
                db.Products.Remove(result);

                db.SaveChanges();

                TempData["ResultMessage"] = $"商品{result.Name}成功編輯";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ResultMessage"] = "指定資料不存在，無法刪除";
                return RedirectToAction("Index");
            }
        }

    }
}