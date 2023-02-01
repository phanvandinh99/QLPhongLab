using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class THONGKEsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/THONGKEs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKe(DateTime Date_From, DateTime Date_to)
        {
            // Hiển thị danh sách phiếu mượn trong khoảng

            // Hiển thị danh sách phiếu trả trong khoảng
            ViewBag.lstPhieuTra = db.PHIEUTRAs.Where(n => n.NGAYTRA == Date_From).ToList();
            return View();
        }
    }
}