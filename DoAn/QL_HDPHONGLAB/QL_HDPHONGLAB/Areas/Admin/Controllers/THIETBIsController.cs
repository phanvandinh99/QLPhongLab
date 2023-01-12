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
    public class THIETBIsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/THIETBIs
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
            var tHIETBI = db.THIETBIs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tHIETBI = tHIETBI.Where(s => s.MATB.Contains(searchString)
                                       || s.TENTB.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.MATB);
                    break;
                case "ten":
                    tHIETBI = tHIETBI.OrderBy(s => s.TENTB);
                    break;
                case "ten_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.TENTB);
                    break;
                default:
                    tHIETBI = tHIETBI.OrderBy(s => s.MATB);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var tHIETBI = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = (from THIETBI in db.THIETBIs select THIETBI.SLTON).Sum();
            return View(tHIETBI.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/THIETBIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Create
        public ActionResult Create()
        {
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL");
            return View();
        }

        // POST: Admin/THIETBIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,TAPHUAN")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.THIETBIs.Add(tHIETBI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,TAPHUAN")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            db.THIETBIs.Remove(tHIETBI);
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

        //KHOA CNTP
        // GET: Admin/THIETBIs/CNTP
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
            var tHIETBI = db.THIETBIs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tHIETBI = tHIETBI.Where(s => s.MATB.Contains(searchString)
                                       || s.TENTB.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.MATB);
                    break;
                case "ten":
                    tHIETBI = tHIETBI.OrderBy(s => s.TENTB);
                    break;
                case "ten_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.TENTB);
                    break;
                default:
                    tHIETBI = tHIETBI.OrderBy(s => s.MATB);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var tHIETBI = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = (from THIETBI in db.THIETBIs select THIETBI.SLTON).Sum();

            return View(tHIETBI.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/THIETBIs/Details/5/CNTP
        public ActionResult Details_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Create/CNTP
        public ActionResult Create_CNTP()
        {
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL");
            return View();
        }

        // POST: Admin/THIETBIs/Create/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,TAPHUAN")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.THIETBIs.Add(tHIETBI);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Edit/5/CNTP
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Edit/5/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,TAPHUAN")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Delete/5/CNTP
        public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Delete/5/CNTP
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            db.THIETBIs.Remove(tHIETBI);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
        }
    }
}
