using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cart2_test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Models.CartContainer db = new Models.CartContainer())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        public ActionResult Details(int id)
        {
            using (Models.CartContainer db = new Models.CartContainer())
            {
                var result = (from s in db.Products
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

        [HttpPost]  //限定使用POST
        public ActionResult AddComment(int id, string Content)
        {
            //取得目前登入使用者Id
            var userId = 1;

            var currentDateTime = DateTime.Now;

            var comment = new Models.ProductComment()
            {
                ProductId = id,
                Content = Content,
                UserId = userId,
                CreateDate = currentDateTime.ToShortDateString()
            };

            using (Models.CartContainer db = new Models.CartContainer())
            {
                db.ProductComment.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}