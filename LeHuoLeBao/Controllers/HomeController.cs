using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeHuoLeBao.Models;
using System.Data;
using System.Text;

namespace LeHuoLeBao.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Logon", "Home");
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Left()
        {
            return View();
        }
        public ActionResult Main()
        {
            return View();
        }

        public void LogOff()//注销
        {
            Session.Abandon();  //把当前Session对象删除了
            Session.Clear();  //把Session对象中的所有项目都删除了
            Response.Redirect("./Logon");// RedirectToAction("Index", "Front"); 

        }
        #region 登录
        public ActionResult Logon()//登录
        {

            return View();
        }
        
        [HttpPost]
        public string LogOn(decimal userid, string pwd)
        {
            try
            {
                using (var db = new HappyEntities())
                {
                    var user = db.dl_basic_users.Where(p => p.userid == userid && p.userpwd == pwd && p.isDel == false).FirstOrDefault();
                    if (user != null)
                    {
                        Session["user"] = user;
                        return "success";
                    }
                    else
                    {
                        return "密码有误！";
                    }
                }
            }
            catch (Exception ex)
            {
                return "用户名有误！" + ex.Message;
            }
        }
        #endregion
	}
}