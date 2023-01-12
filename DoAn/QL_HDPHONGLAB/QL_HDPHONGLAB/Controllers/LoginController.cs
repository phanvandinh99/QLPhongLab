using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class LoginController : Controller
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Login
        public ActionResult DanhSach()
        {
            return View(db.NGUOIDUNGs.ToList());
        }

        [HttpGet]
        public ActionResult FormDangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(NGUOIDUNG nd)
        {
            var check = db.NGUOIDUNGs.Where(s => s.EMAIL.Equals(nd.EMAIL) && s.PASSWORD.Equals(nd.PASSWORD)).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("ShowLoi404", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(NGUOIDUNG nd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = db.NGUOIDUNG.FirstOrDefault(s => s.EMAIL == nd.EMAIL);
        //        if (check == null)
        //        {
        //            nd.PASSWORD = GetMD5(nd.PASSWORD);
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.NGUOIDUNG.Add(nd);
        //            db.SaveChanges();
        //            return RedirectToAction("FormDangNhap", "Login");
        //        }
        //        else
        //        {
        //            ViewBag.error = "Email đã được sử dụng.";
        //            return View();
        //        }
        //    }
        //    return View();
        //}
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
            return RedirectToAction("FormDangNhap", "Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Changepassword(DOIMATKHAU login)
        {
            using (QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities())
            {
                var detail = db.DOIMATKHAUs.Where(log => log.PASSWORD == login.PASSWORD
                && log.EMAIL == login.EMAIL && log.NEWPASSWORD == login.NEWPASSWORD).FirstOrDefault();
                if (detail != null)
                {
                    detail.PASSWORD = login.NEWPASSWORD;
                    detail.NEWPASSWORD = login.CONFIRMPASSWORD;

                    db.SaveChanges();
                    ViewBag.Message = "Cập nhật thành công!";
                    return RedirectToAction("FormDangNhapAfter");

                }
                else
                {
                    ViewBag.Message = "Mật khẩu chưa được cập nhật!";
                }


            }

            return View(login);
        }

        [HttpGet]
        public ActionResult FormDangNhapAfter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthenAfter(DOIMATKHAU change)
        {
            var check = db.DOIMATKHAUs.Where(s => s.EMAIL.Equals(change.EMAIL) && s.PASSWORD.Equals(change.PASSWORD)).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("ShowLoi404", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult ForgotPassword(string emailId)
        //{
        //    string message = "";
        //    bool status = false;
        //    using (QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities())
        //    {
        //        var account = db.NGUOIDUNG.Where(a => a.EMAIL == emailId).FirstOrDefault();
        //        if(account == null)
        //        {
        //            string resetcode = Guid.NewGuid().ToString();

        //        }
        //        else
        //        {
        //            message = "Không tìm thấy tài khoản";
        //        } 
        //    }
        //    return View();
        //}
    }
}