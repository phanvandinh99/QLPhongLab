using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_HDPHONGLAB.Models;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PHONGLABsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/PHONGLABs
        public ActionResult Index()
        {
            return View(db.PHONGLABs.ToList());
        }

        // GET: Admin/PHONGLABs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
            if (pHONGLAB == null)
            {
                return HttpNotFound();
            }
            return View(pHONGLAB);
        }

        // GET: Admin/PHONGLABs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PHONGLABs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPHLAB,TENPHLAB,DIADIEM,GHICHU")] PHONGLAB pHONGLAB)
        {
            if (ModelState.IsValid)
            {
                db.PHONGLABs.Add(pHONGLAB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pHONGLAB);
        }

        // GET: Admin/PHONGLABs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
            if (pHONGLAB == null)
            {
                return HttpNotFound();
            }
            return View(pHONGLAB);
        }

        // POST: Admin/PHONGLABs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPHLAB,TENPHLAB,DIADIEM,GHICHU")] PHONGLAB pHONGLAB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHONGLAB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pHONGLAB);
        }

        // GET: Admin/PHONGLABs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
            if (pHONGLAB == null)
            {
                return HttpNotFound();
            }
            return View(pHONGLAB);
        }

        // POST: Admin/PHONGLABs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
            db.PHONGLABs.Remove(pHONGLAB);
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
