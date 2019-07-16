using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cart2_test.Controllers
{
    public class ManageUserController : Controller
    {
        private Models.UserEntityContainer db = new Models.UserEntityContainer();

        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            //抓取所有AspNetUsers中的資料，並且放入Models.ManageUser模型中
            var result = (from s in db.UserSet
                            select new Models.ManageUser
                            {
                                Id = s.Id,
                                UserName = s.UserName,
                                Email = s.Email
                            }).ToList();
            return View(result);
        }

        // GET: ManageUser/Edit/5
        public ActionResult Edit(int id)
        {
            var result = (from s in db.UserSet
                            where s.Id == id
                            select new Models.ManageUser
                            {
                                Id = s.Id,
                                UserName = s.UserName,
                                Email = s.Email
                            }).FirstOrDefault();
            // 若 User 在 db 
            if (result != default(Models.ManageUser))
            {
                return View(result);
            }
            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", id);

            return RedirectToAction("Index");
        }

        // POST: ManageUser/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {
            var result = (from s in db.UserSet
                            where s.Id == postback.Id
                            select s).FirstOrDefault();
            if (result != default(Models.User))
            {
                result.UserName = postback.UserName;
                result.Email = postback.Email;
                db.SaveChanges();
                //設定成功訊息
                TempData["ResultMessage"] = String.Format("使用者[{0}]成功編輯", postback.UserName);
                return RedirectToAction("Index");
            }

            //設定錯誤訊息
            TempData["ResultMessage"] = String.Format("使用者[{0}]不存在，請重新操作", postback.UserName);
            return RedirectToAction("Index");
        }

    }
}
