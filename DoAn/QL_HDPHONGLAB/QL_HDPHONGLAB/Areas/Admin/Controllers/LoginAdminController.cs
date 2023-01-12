using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            if(Session["user"] == null)
            {
                return RedirectToAction("Dangnhap");
            }    
            return View();
        }

        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(NGUOIDUNG nd)
        {

            var check = db.NGUOIDUNGs.Where(s => s.EMAIL.Equals(nd.EMAIL) && s.PASSWORD.Equals(nd.PASSWORD)).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("HienThiLoi404", "TrangChu");
            }
            else
            {
                return RedirectToAction("Trangchu", "TrangChu");
            }

            //if(username == "admin" && password == "123456")
            //{
            //    Session.Add("user", username);
            //    return RedirectToAction("Trangchu", "TrangChu");
            //}
            //else
            //{
            //    return RedirectToAction("HienThiLoi404", "TrangChu");
            //}
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(NGUOIDUNG nd)
        {
            if (ModelState.IsValid)
            {
                var check = db.NGUOIDUNGs.FirstOrDefault(s => s.EMAIL == nd.EMAIL);
                if (check == null)
                {
                    //nd.PASSWORD = GetMD5(nd.PASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.NGUOIDUNGs.Add(nd);
                    db.SaveChanges();
                    return RedirectToAction("Trangchu", "TrangChu");
                }
                else
                {
                    ViewBag.error = "Email đã được sử dụng.";
                    return View();
                }
            }
            return View();
        }
        ////create a string MD5
        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Dangnhap", "LoginAdmin");
        }
    }
} 