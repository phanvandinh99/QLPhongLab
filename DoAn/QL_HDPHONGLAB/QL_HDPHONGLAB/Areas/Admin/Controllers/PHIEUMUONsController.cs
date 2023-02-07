using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using PagedList;
using QL_HDPHONGLAB.Model;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PHIEUMUONsController : Controller
    {
        private QL_HDPHONGLABEntities3 db = new QL_HDPHONGLABEntities3();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            ViewBag.TenSortParm = sortOrder == "ten" ? "ten_desc" : "ten";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var phieumuon = db.PHIEUMUON.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                phieumuon = phieumuon.Where(s => s.HOACHAT.TENHC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    phieumuon = phieumuon.OrderByDescending(s => s.MAPM);
                    break;
                case "ten":
                    phieumuon = phieumuon.OrderBy(s => s.MAPM);
                    break;
                case "ten_desc":
                    phieumuon = phieumuon.OrderByDescending(s => s.MAPM);
                    break;
                default:
                    phieumuon = phieumuon.OrderBy(s => s.MAPM);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var phieumuon = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = db.PHIEUMUON.Count();
            return View(phieumuon.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/PHIEUMUONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUMUON pHIEUMUON = db.PHIEUMUON.Find(id);
            if (pHIEUMUON == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUMUON);
        }

        // GET: Admin/PHIEUMUONs/Create
        public ActionResult Create()
        {
            ViewBag.MAPHLAB = new SelectList(db.PHONGLAB, "MAPHLAB", "TENPHLAB");
            ViewBag.MAHC = new SelectList(db.HOACHAT, "MAHC", "TENHC");

            #region Tự động tăng mã phiếu mượn
            if (db.PHIEUMUON.Count() != 0)
            {
                // Lấy ra mã hóa chất (HC01)
                int hoachat = db.PHIEUMUON.Max(s => s.MAPM);
                int stt = hoachat + 1;
                ViewBag.TTMAPM = stt;
            }
            else
            {
                ViewBag.TTMAPT = "1";
            }
            #endregion

            return View();
        }

        // POST: Admin/PHIEUMUONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPM,NGAYMUON,NGAYTRA,NOIDUNG,MAHC,NGUOITRA,MAPHLAB,TU,DEN,TRANGTHAI,GHICHU,SLMUON")] PHIEUMUON pHIEUMUON)
        {
            if (ModelState.IsValid)
            {
                // Trừ số lượng trong kho hóa chất
                var hoaChat = db.HOACHAT.SingleOrDefault(n => n.MAHC == pHIEUMUON.MAHC);
                hoaChat.LUONGTON = hoaChat.LUONGTON - pHIEUMUON.SLMUON;
                hoaChat.LUONGXUAT += Convert.ToInt32(pHIEUMUON.SLMUON);

                db.PHIEUMUON.Add(pHIEUMUON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPHLAB = new SelectList(db.PHONGLAB, "MAPHLAB", "TENPHLAB", pHIEUMUON.MAPHLAB);
            return View(pHIEUMUON);
        }

        // GET: Admin/PHIEUMUONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUMUON pHIEUMUON = db.PHIEUMUON.Find(id);
            if (pHIEUMUON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPHLAB = new SelectList(db.PHONGLAB, "MAPHLAB", "TENPHLAB", pHIEUMUON.MAPHLAB);
            return View(pHIEUMUON);
        }

        // POST: Admin/PHIEUMUONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPM,NGAYMUON,NGAYTRA,NOIDUNG,MAHC,NGUOITRA,MAPHLAB,TU,DEN,TRANGTHAI,GHICHU")] PHIEUMUON pHIEUMUON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUMUON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPHLAB = new SelectList(db.PHONGLAB, "MAPHLAB", "TENPHLAB", pHIEUMUON.MAPHLAB);
            return View(pHIEUMUON);
        }

        // GET: Admin/PHIEUMUONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUMUON pHIEUMUON = db.PHIEUMUON.Find(id);
            if (pHIEUMUON == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUMUON);
        }

        // POST: Admin/PHIEUMUONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUMUON pHIEUMUON = db.PHIEUMUON.Find(id);
            db.PHIEUMUON.Remove(pHIEUMUON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Xuất phiếu mượn = excel
        public ActionResult ExcelExport()
        {
            try
            {
                var lstPhieuTra = db.PHIEUMUON.OrderBy(n => n.MAPM).ToList();

                DataTable Dt = new DataTable();
                Dt.Columns.Add("Mã Phiếu Mượn", typeof(string));
                Dt.Columns.Add("Ngày Mượn", typeof(string));
                Dt.Columns.Add("Ngày Trả", typeof(string));
                Dt.Columns.Add("Nội Dung", typeof(string));
                Dt.Columns.Add("Hóa Chất", typeof(string));
                Dt.Columns.Add("Người Mượn", typeof(string));
                Dt.Columns.Add("Phòng Lab", typeof(string));
                Dt.Columns.Add("Từ", typeof(string));
                Dt.Columns.Add("Đến", typeof(string));
                Dt.Columns.Add("Ghi Chú", typeof(string));

                foreach (var data in lstPhieuTra)
                {
                    DataRow row = Dt.NewRow();
                    row[0] = data.MAPM;
                    row[1] = data.NGAYMUON;
                    row[2] = data.NGAYTRA;
                    row[3] = data.NOIDUNG;
                    row[4] = data.HOACHAT.TENHC;
                    row[5] = data.NGAYMUON;
                    row[6] = data.PHONGLAB.TENPHLAB;
                    row[7] = data.TU;
                    row[8] = data.DEN;
                    row[9] = data.GHICHU;
                    Dt.Rows.Add(row);
                }

                var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("BẢNG PHIẾU MƯỢN");
                    worksheet.Cells["A1"].Value = "PHIẾU MƯỢN";
                    worksheet.Cells["A2"].LoadFromDataTable(Dt, true, TableStyles.None);
                    worksheet.Cells["A2:AN2"].Style.Font.Bold = true;
                    worksheet.DefaultRowHeight = 18;


                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.DefaultColWidth = 20;
                    worksheet.Column(2).AutoFit();

                    Session["DownloadExcel_FileManager"] = excelPackage.GetAsByteArray();
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            if (Session["DownloadExcel_FileManager"] != null)
            {
                byte[] data = Session["DownloadExcel_FileManager"] as byte[];
                return File(data, "application/octet-stream", "FileManager.xlsx");
            }
            else
            {
                return new EmptyResult();
            }
        }
    }
}
