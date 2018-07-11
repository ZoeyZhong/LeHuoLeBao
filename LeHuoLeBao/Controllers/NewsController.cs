using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeHuoLeBao.Models;
using System.Text;
using Telerik.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using Newtonsoft.Json;

namespace LeHuoLeBao.Controllers
{
    public class NewsController : Controller
    {
        #region 新建新闻
        
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /News/ 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection fc)
        {
            
            //string content = Request.Form["editor"];
            //return View();
            ////var title = SaveTitle(json);
            
            string data = Request.Form["title"];
            string content = fc["editor"];
            System.DateTime starttime = new System.DateTime();
            starttime = DateTime.Now;
            if (data == "")
            {
                ViewData["back_news"] = "标题为空，请重新输入";
                return View();
            }
            else if (content == "")
            {
                ViewData["back_news"] = "内容为空，请重新输入";
                return View();
            }
            else
            { 
                try
                {
                    using (var db = new HappyEntities())
                    {
                        var news = new t_f_news()
                        {
                            title = data,
                            content = content,
                            time = starttime,
                            isDel = false
                        };
                        db.t_f_news.Add(news);
                        db.SaveChanges();
                        ViewData["back_news"] = "保存成功！";
                        return View(); 
                    }
                }
                catch (Exception ex)
                {
                    ViewData["back_news"] = "保存失败";
                    return View(); 
                }
            }
        }
       #endregion
        #region 新闻列表
        public ActionResult News()
        { 
            return View();
        }
        [GridAction]
        public ActionResult NewsList()
        {
            var db = new HappyEntities();
            var data = from a in db.t_f_news
                       where a.isDel == false
                       orderby a.newsid ascending
                       select new NewsCom
                       {
                           newsid = a.newsid,
                           time = a.time,
                           title = a.title,
                           content = a.content,
                       };
            return View(new GridModel() 
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 编辑新闻
        public ActionResult EditNews(string id)
        {
            HappyEntities db = new HappyEntities();
            List<SelectListItem> recuit = new List<SelectListItem>();
            decimal newsid = decimal.Parse(id);
            t_f_news dd = db.t_f_news.Where(s => s.newsid == newsid).First();
            ViewData["newsid"] = dd.newsid;//编号ID
            ViewData["time"] = dd.time;//时间
            ViewData["title1"] = dd.title;//标题
            ViewData["content"] = dd.content;//内容
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNews(FormCollection fc)
        {

            //string content = Request.Form["editor"];
            //return View();
            ////var title = SaveTitle(json);
            decimal newsid = Decimal.Parse(Request.Form["newsid"]);
            string data = Request.Form["title"];
            string content = fc["editor"];
            System.DateTime starttime = new System.DateTime();
            starttime = DateTime.Now;
            if (data == "")
            {
                ViewData["back_news"] = "标题为空，请重新输入";
                return View();
            }
            else if (content == "")
            {
                ViewData["back_news"] = "内容为空，请重新输入";
                return View();
            }
            else
            {
                try
                {
                    using (var db = new HappyEntities())
                    {

                        var recruit = db.t_f_news.FirstOrDefault(s => s.newsid == newsid);
                        recruit.title = data;
                        recruit.content = content;
                        recruit.time = starttime;
                        db.SaveChanges();
                        ViewData["back_news"] = "保存成功！";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewData["back_news"] = "保存失败";
                    return View();
                }
            }
        }
        #endregion
        #region 删除新闻
        public ActionResult DeleteNews(string id)
        {
            var db = new HappyEntities();
            decimal newsid = decimal.Parse(id);
            var q = db.t_f_news.FirstOrDefault(m => m.newsid == newsid);
            q.isDel = true;
            db.SaveChanges();
            var data = from a in db.t_f_news
                       where a.isDel == false
                       orderby a.newsid ascending
                       select new NewsCom
                       {
                           newsid = a.newsid,
                           time = a.time,
                           title = a.title,
                           content = a.content,
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
    }
}