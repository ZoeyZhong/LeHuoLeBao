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
    public class ContentController : Controller
    {
        //
        // GET: /Content/
        public ActionResult Index()
        {
            return View();
        }
        #region 新增商业合作
        public ActionResult CreatPark()
        {
            return View();
        }
        [HttpPost]// 保存new
        public string SavePark(string json)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            JsonObject obj = new JsonObject(json);
            string option2 = obj["option2"].Value.ToString();//需求类别
            string p_title = obj["p_title"].Value.ToString();//需求名称
            string option1 = obj["option1"].Value.ToString();//数量
            string p_content = obj["p_content"].Value.ToString();//详细内容
            try
            {
                using (var db = new HappyEntities())
                {
                    var person = new t_f_park()
                    {
                        option2=option2,
                        p_title=p_title,
                        option1=option1,
                        p_content=p_content,
                        isDel = false
                    };
                    db.t_f_park.Add(person);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region 招商信息列表
        public ActionResult Park()
        {
            return View();
        }
        [GridAction]
        public ActionResult ParkList()
        {
            var db = new HappyEntities();
            var data = from a in db.t_f_park
                       where a.isDel == false
                       orderby a.park_id ascending
                       select new ParkCom
                       {
                           park_id = a.park_id,
                           p_title = a.p_title,//需求
                           p_content = a.p_content,//详细内容
                           option1  = a.option1,//数量
                           option2 = a.option2//类别
                       };
            return View(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 编辑招商信息
        public ActionResult EditPark(string id)
        {
            HappyEntities db = new HappyEntities();
            List<SelectListItem> recuit = new List<SelectListItem>();
            decimal park_id = decimal.Parse(id);
            t_f_park dd = db.t_f_park.Where(s => s.park_id == park_id).First();
            ViewData["park_id"] = dd.park_id;//编号ID
            ViewData["option2"] = dd.option2;//需求类别
            ViewData["p_title"] = dd.p_title;//名称
            ViewData["option1"] = dd.option1;//数量
            ViewData["p_content"] = dd.p_content;//详细内容
            return View();
        }
        [HttpPost]//保存edit
        public string SaveEditP(string json)
        {
            JsonObject obj = new JsonObject(json);
            decimal park_id = decimal.Parse(obj["park_id"].Value.ToString());
            string option2 = obj["option2"].Value.ToString();
            string p_title = obj["p_title"].Value.ToString();
            string option1 = obj["option1"].Value.ToString();
            string p_content = obj["p_content"].Value.ToString();
            try
            {
                using (var db = new HappyEntities())
                {
                    var recruit = db.t_f_park.FirstOrDefault(s => s.park_id == park_id);
                    recruit.option2 = option2;
                    recruit.p_title = p_title;
                    recruit.option1 = option1;
                    recruit.p_content = p_content;
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region 删除招商信息
        public ActionResult DeletePark(string id)
        {
            var db = new HappyEntities();
            decimal park_id = decimal.Parse(id);
            var a = db.t_f_park.FirstOrDefault(m => m.park_id == park_id);
            a.isDel = true;
            db.SaveChanges();
            var data = from b in db.t_f_park
                       where b.isDel == false
                       orderby b.park_id ascending
                       select new ParkCom
                       {
                           park_id = a.park_id,
                           option2 = a.option2,
                           p_title = a.p_title,
                           option1 = a.option1,
                           p_content = a.p_content
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion

        #region 新增招聘信息
        public ActionResult Creatrecruit()//新增招聘人员
        {
            return View();
        }
        [HttpPost]// 保存new 招聘职位
        public string Save(string json)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            JsonObject obj = new JsonObject(json);
            string re_name = obj["re_name"].Value.ToString();//招聘职位名字
            string re_type = obj["re_type"].Value.ToString();//招聘职位类型
            string re_money = obj["re_money"].Value.ToString();//薪金
            string re_require = obj["re_require"].Value.ToString();//招聘要求
            try
            {
                using (var db = new HappyEntities())
                {
                    var person = new t_f_recruit()
                    {
                        re_name = re_name,
                        re_type = re_type,
                        re_money = re_money,
                        re_require = re_require,
                        isDel = false
                    };
                    db.t_f_recruit.Add(person);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region 招聘信息列表
        public ActionResult Recuit()
        {
            return View();
        }
        [GridAction]//商品列表
        public ActionResult RecuitList()
        {
            var db = new HappyEntities();
            var data = from a in db.t_f_recruit
                       where a.isDel == false
                       orderby a.recruitid ascending
                       select new RecuitCom
                       {
                           recruitid = a.recruitid,
                           re_name = a.re_name,
                           re_type = a.re_type,
                           re_money = a.re_money,
                           re_require = a.re_require
                       };
            return View(new GridModel() 
            {
                Data = data,
                Total = data.Count()
            });
        }
        
        #endregion
        #region 编辑招聘信息
        public ActionResult EditRecruit(string id)
        {
            HappyEntities db = new HappyEntities();
            List<SelectListItem> recuit = new List<SelectListItem>();
            decimal recruitid = decimal.Parse(id);
            t_f_recruit dd = db.t_f_recruit.Where(s => s.recruitid == recruitid).First();
            ViewData["recruitid"] = dd.recruitid;//招聘信息ID
            ViewData["re_type"] = dd.re_type;//招聘岗位类别
            ViewData["re_name"] = dd.re_name;//招聘岗位名称
            ViewData["re_money"] = dd.re_money;//招聘岗位薪金
            ViewData["re_require"] = dd.re_require;//岗位要求
            return View();
        }
        [HttpPost]//保存edit
        public string SaveEditR(string json)
        {
            JsonObject obj = new JsonObject(json);
            decimal recruitid = decimal.Parse(obj["recruitid"].Value.ToString());
            string re_type = obj["re_type"].Value.ToString();
            string re_name = obj["re_name"].Value.ToString();
            string re_money = obj["re_money"].Value.ToString();
            string re_require = obj["re_require"].Value.ToString();
            try
            {
                using (var db = new HappyEntities())
                {
                    var recruit = db.t_f_recruit.FirstOrDefault(s => s.recruitid == recruitid);
                    recruit.re_type = re_type;
                    recruit.re_name = re_name;
                    recruit.re_money = re_money;
                    recruit.re_require = re_require;
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region 删除招聘信息
        public ActionResult DeleteRecruit(string id)
        {
            var db = new HappyEntities();
            decimal recruitid = decimal.Parse(id);
            var a = db.t_f_recruit.FirstOrDefault(m => m.recruitid == recruitid);
            a.isDel = true;
            db.SaveChanges();
            var data = from b in db.t_f_recruit
                       where b.isDel == false
                       orderby b.recruitid ascending
                       select new RecuitCom
                       {
                           recruitid = a.recruitid,
                           re_name = a.re_name,
                           re_type = a.re_type,
                           re_money = a.re_money,
                           re_require = a.re_require
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