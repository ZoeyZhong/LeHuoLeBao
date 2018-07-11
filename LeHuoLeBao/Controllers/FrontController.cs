using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeHuoLeBao.Models;
using Newtonsoft.Json;
using System.Text;
using Telerik.Web.Mvc;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace LeHuoLeBao.Controllers
{
    public class FrontController : Controller
    {
        //
        // GET: /Front/
        public ActionResult Index()//首页
        {
            return View();
        }
        public ActionResult Park()//商业合作
        {
            return View();
        }
        public ActionResult News()//新闻动态
        {
            return View();
        }
        public ActionResult Ticket()//购票
        {
            return View();
        }
        public ActionResult Recruit()//招聘
        {
            return View();
        }
        #region 留言
        public ActionResult About()//关于我们
        {
            return View();
        }
        #endregion

        #region 新闻内容页

        public ActionResult Newscontent(string theid)
        {
            HappyEntities db = new HappyEntities();
            decimal newsid1 = decimal.Parse(theid);
            t_f_news dd = db.t_f_news.Where(s => s.newsid == newsid1).First();
            ViewData["newsid"] = dd.newsid;//编号ID
            ViewData["time"] = dd.time;//时间
            ViewData["title1"] = dd.title;//标题
            ViewData["content"] = dd.content;//内容
            return View();
        }
        #endregion
    }
}