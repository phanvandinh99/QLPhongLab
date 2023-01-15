using OfficeOpenXml;
using OfficeOpenXml.Table;
using PagedList;
using QL_HDPHONGLAB.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class HOACHATsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/HOACHATs
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
            var hOACHAT = db.HOACHATs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                hOACHAT = hOACHAT.Where(s => s.MAHC.Contains(searchString)
                                       || s.TENHC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.MAHC);
                    break;
                case "ten":
                    hOACHAT = hOACHAT.OrderBy(s => s.TENHC);
                    break;
                case "ten_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.TENHC);
                    break;
                default:
                    hOACHAT = hOACHAT.OrderBy(s => s.MAHC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var hOACHAT = db.HOACHAT.Include(h => h.CT_HOACHAT);
            ViewBag.total = (from HOACHAT in db.HOACHATs select HOACHAT.LUONGTON).Sum();
            return View(hOACHAT.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HOACHATs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Create
        public ActionResult Create()
        {
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU");

            ViewBag.MaLoaiHoaChat = db.LOAIHOACHATs.ToList();

            #region Tự động tăng mã hóa chất
            // Lấy ra mã hóa chất (HC01)
            var hoachat = db.HOACHATs.OrderByDescending(n => n.MAHC.Substring(2, n.MAHC.Length)).First();
            int stt = int.Parse(hoachat.MAHC.Substring(2)) + 1;
            ViewBag.TTMAHC = "HC" + stt;
            #endregion

            return View();
        }

        // POST: Admin/HOACHATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON,MALHC")] HOACHAT hOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.HOACHATs.Add(hOACHAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // POST: Admin/HOACHATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON")] HOACHAT hOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOACHAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        // POST: Admin/HOACHATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            db.HOACHATs.Remove(hOACHAT);
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

        //KHOA CÔNG NGHIỆP THỰC PHẨM
        //GET: Admin/HOACHATs/CNTP/Index

        public ActionResult Index_CNTP(string sortOrder, string currentFilter, string searchString, int? page)
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
            var hOACHAT = db.HOACHATs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                hOACHAT = hOACHAT.Where(s => s.MAHC.Contains(searchString)
                                       || s.TENHC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.MAHC);
                    break;
                case "ten":
                    hOACHAT = hOACHAT.OrderBy(s => s.TENHC);
                    break;
                case "ten_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.TENHC);
                    break;
                default:
                    hOACHAT = hOACHAT.OrderBy(s => s.MAHC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var hOACHAT = db.HOACHAT.Include(h => h.CT_HOACHAT);
            ViewBag.total = (from HOACHAT in db.HOACHATs select HOACHAT.LUONGTON).Sum();
            return View(hOACHAT.ToPagedList(pageNumber, pageSize));
        }

        //GET: Admin/HOACHATs/Details/CNTP
        public ActionResult Detail_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        //GET: Admin/HOACHATs/Create/CNTP
        public ActionResult Create_CNTP()
        {
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU");
            return View();
        }

        // POST: Admin/HOACHATs/Create/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON")] HOACHAT hOACHAT, CT_HOACHAT cT)
        {
            if (ModelState.IsValid)
            {
                db.HOACHATs.Add(hOACHAT);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Edit/5/CNTP
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // POST: Admin/HOACHATs/Edit/5/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON")] HOACHAT hOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOACHAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Delete/5
        public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        // POST: Admin/HOACHATs/Delete/5/CNTP
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            db.HOACHATs.Remove(hOACHAT);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
        }

        // Xuất hóa chất = excel
        public ActionResult ExcelExport()
        {
            try
            {
                var lstHoaChat = db.HOACHATs.OrderBy(n => n.MAHC).ToList();

                DataTable Dt = new DataTable();
                Dt.Columns.Add("Mã Hóa Chất", typeof(string));
                Dt.Columns.Add("Tên Hóa Chất", typeof(string));
                Dt.Columns.Add("Loại Hóa Chất", typeof(string));
                Dt.Columns.Add("Thông Số", typeof(string));
                Dt.Columns.Add("CASNO", typeof(string));
                Dt.Columns.Add("Đơn Vị", typeof(string));
                Dt.Columns.Add("Lượng Nhập", typeof(string));
                Dt.Columns.Add("Lượng Xuất", typeof(string));
                Dt.Columns.Add("Lượng Tồn", typeof(string));
                Dt.Columns.Add("Lượng Thanh Lý", typeof(string));
                Dt.Columns.Add("Giá Nhập", typeof(string));

                foreach (var data in lstHoaChat)
                {
                    DataRow row = Dt.NewRow();
                    row[0] = data.MAHC;
                    row[1] = data.TENHC;
                    row[2] = data.LOAIHOACHAT.TENLHC;
                    row[3] = data.THONGSO;
                    row[4] = data.CASNO;
                    row[5] = data.DONVI;
                    row[6] = data.LUONGNHAP;
                    row[7] = data.LUONGXUAT;
                    row[8] = data.LUONGTON;
                    row[9] = data.LUONGTHANHLY;
                    row[10] = data.GIANHAP;
                    Dt.Rows.Add(row);
                }

                var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage(memoryStream))
                {
                    var worksheet = excelPackage.Workbook.Worksheets.Add("BẢNG HÓA CHẤT");
                    worksheet.Cells["A1"].Value = "HÓA CHẤT";
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
