using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cart2_test.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if (this.ModelState.IsValid)
            {   //取得目前購物車
                var currentcart = Models.Cart.Operation.GetCurrentCart();

                //取得目前登入使用者Id
                //var userId = HttpContext.User.Identity.GetUserId();
                int userId = 1;

                using (Models.CartContainer db = new Models.CartContainer())
                {
                    //建立Order物件
                    var order = new Models.Order()
                    {
                        UserId = userId,
                        RecieverName = postback.RecieverName,
                        RecieverPhone = postback.RecieverPhone,
                        RecieverAddress = postback.RecieverAddress
                    };
                    //加其入Orders資料表後，儲存變更
                    db.Order.Add(order);
                    db.SaveChanges();

                    //取得購物車中OrderDetail物件
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);

                    //將其加入OrderDetails資料表後，儲存變更
                    db.OrderDetail.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("訂購成功");
            }
            return View();
        }
    }
}