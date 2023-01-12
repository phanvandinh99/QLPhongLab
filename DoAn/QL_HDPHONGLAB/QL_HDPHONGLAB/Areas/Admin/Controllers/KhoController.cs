using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class KhoController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Admin/Kho
        #region Nhập Kho
        public ActionResult NhapKho()
        {
            ViewBag.ListDTHoaChat = db.DUTRUHOACHATs.ToList();
            ViewBag.All = db.DUTRUHOACHATs.Count();
            ViewBag.HoaChat = db.HOACHATs.ToList().OrderBy(n => n.TENHC);
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.NhaCungCap = db.NCCs.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult NhapKho(PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstModel)
        {
            double? TongTien = 0;
            if (model.NGAYNHAP == null)
            {
                model.NGAYNHAP = DateTime.Now;

            }
            db.PHIEUNHAPs.Add(model);
            db.SaveChanges();

            // Lấy mã phiếu nhập lưu dữ liệu để gán cho list chi tiết phiếu nhập
            foreach (var item in lstModel) // chi tiết phiếu nhập
            {
                // Gán mã phiếu nhập cho cả chi tiết phiếu nhập
                item.MAPN = model.MAPN;
                item.THANHTIEN = (item.SOLUONGNHAP * item.GIANHAP);
                TongTien = TongTien + item.THANHTIEN;

                #region Cộng số lượng hiện có của hoá chất
                var slhc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
           

                slhc.LUONGTON += item.SOLUONGNHAP;
                slhc.GIANHAP = item.GIANHAP;
                #endregion
            }
            db.CHITIETPHIEUNHAPs.AddRange(lstModel);
            PHIEUNHAP pn = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == model.MAPN);
            pn.TONGTIEN = (double)TongTien;
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhap", "PhieuNhap");
        }

        #endregion
    }
}