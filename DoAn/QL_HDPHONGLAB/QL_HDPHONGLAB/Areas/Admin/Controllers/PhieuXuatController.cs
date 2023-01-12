using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuXuatController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // Danh sách phiếu xuất kho
        public ActionResult DanhSachPhieuXuat()
        {
            var listPhieuXuat = db.CHITIETPHIEUXUATs.ToList().OrderByDescending(n => n.MACTPX);
            return View(listPhieuXuat);
        }

        // Xem chi tiết phiếu xuất cho khoa
        public ActionResult XemChiTiet(int iMACTPX)
        {
            ViewBag.CTPX = db.CHITIETPHIEUXUATs.SingleOrDefault(n => n.MACTPX == iMACTPX);
            var chitiet = db.XUATHOACHATs.Where(n => n.MACTPX == iMACTPX);
            return View(chitiet);
        }

        // Xoá phiếu xuất ra khỏi danh sách phiếu xuất
        public ActionResult XoaPhieuXuat(int iMACTPX)
        {
            var listXuatHC = db.XUATHOACHATs.Where(n => n.MACTPX == iMACTPX);
            if(listXuatHC != null)
            {
                foreach(var item in listXuatHC)
                {
                    db.XUATHOACHATs.Remove(item);
                }    
            }

            var phieuxuat = db.CHITIETPHIEUXUATs.SingleOrDefault(n => n.MACTPX == iMACTPX);
            db.CHITIETPHIEUXUATs.Remove(phieuxuat);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuXuat");
        }

        #region Thêm mới vào phiếu xuất
        [HttpGet]
        public ActionResult ThemPhieuXuat()
        {
            ViewBag.HoaChat = db.HOACHATs.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ThemPhieuXuat(CHITIETPHIEUXUAT model, IEnumerable<XUATHOACHAT> lstModel)
        {
            #region Lưu phiếu xuất vào database
            CHITIETPHIEUXUAT px = new CHITIETPHIEUXUAT();
            if(model.NGAYXUAT == null)
            {
                px.NGAYXUAT = DateTime.Now;
            }
            else
            {
                px.NGAYXUAT = model.NGAYXUAT;
            }
            db.CHITIETPHIEUXUATs.Add(px);
            #endregion

            // Lấy mã phiếu xuất
            foreach (var item in lstModel) // chi tiết phiếu xuất
            {

                // tìm mã hoá chất
                var hoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC_id);
                if (hoachat.MALHC == 4) // mã nguyên hoá chất == 4
                {
                    int SL;
                    if (int.TryParse(item.SOLUONGXUAT.ToString(), out SL))
                    {
                        XUATHOACHAT xhc = new XUATHOACHAT();
                        xhc.MACTPX = model.MACTPX;
                        xhc.MAHC_id = item.MAHC_id;
                        xhc.SOLUONGXUAT = item.SOLUONGXUAT;
                        db.XUATHOACHATs.Add(xhc);

                        #region Trừ bớt số lượng trong kho
                        var slhc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC_id);
                        if (slhc.LUONGTON >= item.SOLUONGXUAT)
                        {
                            slhc.LUONGTON = slhc.LUONGTON - item.SOLUONGXUAT;
                        }
                        else
                        {
                            // Thoát và k lưu
                            return RedirectToAction("HienThiLoi500", "TrangChu");
                        }
                        #endregion
                    }
                    else
                    {

                    }
                }
                else
                {
                    XUATHOACHAT xhc = new XUATHOACHAT();
                    xhc.MACTPX = model.MACTPX;
                    xhc.MAHC_id = item.MAHC_id;
                    xhc.SOLUONGXUAT = item.SOLUONGXUAT;
                    db.XUATHOACHATs.Add(xhc);

                    #region Trừ bớt số lượng trong kho
                    var slHC = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC_id);
                    if (slHC.LUONGTON >= item.SOLUONGXUAT)
                    {
                        slHC.LUONGTON = slHC.LUONGTON - item.SOLUONGXUAT;
                    }
                    else
                    {
                        // Thoát và k lưu
                        //db.XuatKhoes.Remove(maXuatKho);
                        return RedirectToAction("HienThiLoi500", "TrangChu");
                    }

                }
                #endregion
            }
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuXuat");
        }
        #endregion
    }
}