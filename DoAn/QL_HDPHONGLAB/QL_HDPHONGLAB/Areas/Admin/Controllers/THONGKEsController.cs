using QL_HDPHONGLAB.Models;
using QL_HDPHONGLAB.Model;
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
        private QL_HDPHONGLABEntities3 db3 = new QL_HDPHONGLABEntities3();


        // GET: Admin/THONGKEs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongKe(DateTime Date_From, DateTime Date_to)
        {
            // Hiển thị danh sách phiếu mượn trong khoảng
            ViewBag.lstPhieuMuon = db3.PHIEUMUON.Where(n => DateTime.Compare(n.NGAYMUON, Date_From) > 0 & DateTime.Compare(n.NGAYTRA, Date_to) < 0).ToList();
            ViewBag.total_PhieuMuon = db3.PHIEUMUON.Where(n => DateTime.Compare(n.NGAYMUON, Date_From) > 0 & DateTime.Compare(n.NGAYTRA, Date_to) < 0).ToList().Count();

            // Hiển thị danh sách phiếu trả trong khoảng
            ViewBag.lstPhieuTra = db.PHIEUTRAs.Where(n => DateTime.Compare(n.NGAYTRA, Date_From) > 0).ToList();
            ViewBag.total_PhieuTra = db.PHIEUTRAs.Where(n => DateTime.Compare(n.NGAYTRA, Date_From) > 0).ToList().Count();

            return View();
        }
    }
}