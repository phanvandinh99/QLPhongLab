using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QL_HDPHONGLAB.Models;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PHIEUTRAsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/PHIEUTRAs
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
            var phieuTra = db.PHIEUTRAs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                phieuTra = phieuTra.Where(s => s.HOACHAT.TENHC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    phieuTra = phieuTra.OrderByDescending(s => s.MAPT);
                    break;
                case "ten":
                    phieuTra = phieuTra.OrderBy(s => s.MAPT);
                    break;
                case "ten_desc":
                    phieuTra = phieuTra.OrderByDescending(s => s.MAPT);
                    break;
                default:
                    phieuTra = phieuTra.OrderBy(s => s.MAPT);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var phieuTra = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = db.PHIEUTRAs.Count();
            return View(phieuTra.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/PHIEUTRAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRA pHIEUTRA = db.PHIEUTRAs.Find(id);
            if (pHIEUTRA == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTRA);
        }

        // GET: Admin/PHIEUTRAs/Create
        public ActionResult Create()
        {
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "TENHC");
            ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB");
            return View();
        }

        // POST: Admin/PHIEUTRAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPT,NGAYTRA,NOIDUNG,MAHC,NGUOITRA,MAPHLAB,TU,DEN,GHICHU")] PHIEUTRA pHIEUTRA)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUTRAs.Add(pHIEUTRA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "TENHC", pHIEUTRA.MAHC);
            ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", pHIEUTRA.MAPHLAB);
            return View(pHIEUTRA);
        }

        // GET: Admin/PHIEUTRAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRA pHIEUTRA = db.PHIEUTRAs.Find(id);
            if (pHIEUTRA == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "TENHC", pHIEUTRA.MAHC);
            ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", pHIEUTRA.MAPHLAB);
            return View(pHIEUTRA);
        }

        // POST: Admin/PHIEUTRAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPT,NGAYTRA,NOIDUNG,MAHC,NGUOITRA,MAPHLAB,TU,DEN,GHICHU")] PHIEUTRA pHIEUTRA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUTRA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "TENHC", pHIEUTRA.MAHC);
            ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", pHIEUTRA.MAPHLAB);
            return View(pHIEUTRA);
        }

        // GET: Admin/PHIEUTRAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTRA pHIEUTRA = db.PHIEUTRAs.Find(id);
            if (pHIEUTRA == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTRA);
        }

        // POST: Admin/PHIEUTRAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUTRA pHIEUTRA = db.PHIEUTRAs.Find(id);
            db.PHIEUTRAs.Remove(pHIEUTRA);
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
    }
}
