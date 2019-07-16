using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cart2_test.Controllers
{
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        public ActionResult Index()
        {
            // 取得所有商品
            var result = Models.RouteTest.TempProducts.GetAllProducts();
            // 傳給View
            return View(result);
        }
    }
}