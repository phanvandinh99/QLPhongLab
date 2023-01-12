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
    public class DUNGCUsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/DUNGCUs
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
            var dUNGCU = db.DUNGCUs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dUNGCU = dUNGCU.Where(s => s.MADC.Contains(searchString)
                                       || s.TENDC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.MADC);
                    break;
                case "ten":
                    dUNGCU = dUNGCU.OrderBy(s => s.TENDC);
                    break;
                case "ten_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.TENDC);
                    break;
                default:
                    dUNGCU = dUNGCU.OrderBy(s => s.MADC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var dUNGCU = db.DUNGCU.Include(d => d.CT_DUNGCU);
            ViewBag.total = (from DUNGCU in db.DUNGCUs select DUNGCU.LUONGTON).Sum();

            return View(dUNGCU.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DUNGCUs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Create
        public ActionResult Create()
        {

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU");
            

            return View();
        }

        // POST: Admin/DUNGCUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,DVT,NGAYNHAP,GIOSD")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.DUNGCUs.Add(dUNGCU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,DVT,NGAYNHAP,GIOSD")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUNGCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            db.DUNGCUs.Remove(dUNGCU);
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


        // KHOA CNTP
        // GET: Admin/DUNGCUs/CNTP
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
            var dUNGCU = db.DUNGCUs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dUNGCU = dUNGCU.Where(s => s.MADC.Contains(searchString)
                                       || s.TENDC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.MADC);
                    break;
                case "ten":
                    dUNGCU = dUNGCU.OrderBy(s => s.TENDC);
                    break;
                case "ten_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.TENDC);
                    break;
                default:
                    dUNGCU = dUNGCU.OrderBy(s => s.MADC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var dUNGCU = db.DUNGCU.Include(d => d.CT_DUNGCU);
            ViewBag.total = (from DUNGCU in db.DUNGCUs select DUNGCU.LUONGTON).Sum();
            return View(dUNGCU.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DUNGCUs/Details/5/CNTP
        public ActionResult Details_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Create
        public ActionResult Create_CNTP()
        {
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU");
            return View();
        }

        // POST: Admin/DUNGCUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,DVT,LUONGNHAP,NGAYNHAP,GIOSD")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.DUNGCUs.Add(dUNGCU);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Edit/5
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Edit/5/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,DVT,LUONGNHAP,NGAYNHAP,GIOSD")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUNGCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Delete/5
        public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Delete/5
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            db.DUNGCUs.Remove(dUNGCU);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
        }
    }
}
