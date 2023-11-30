using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhaHangPIzza.Models;

namespace NhaHangPIzza.Areas.Admin.Controllers
{
    public class KichThuocBanhsController : Controller
    {
        private QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/KichThuocBanhs
        public ActionResult Index()
        {
            return View(db.KichThuocBanhs.ToList());
        }

        // GET: Admin/KichThuocBanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichThuocBanh kichThuocBanh = db.KichThuocBanhs.Find(id);
            if (kichThuocBanh == null)
            {
                return HttpNotFound();
            }
            return View(kichThuocBanh);
        }

        // GET: Admin/KichThuocBanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KichThuocBanhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDKichThuocBanh,KichThuoc,GiaTien")] KichThuocBanh kichThuocBanh)
        {
            if (ModelState.IsValid)
            {
                db.KichThuocBanhs.Add(kichThuocBanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kichThuocBanh);
        }

        // GET: Admin/KichThuocBanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichThuocBanh kichThuocBanh = db.KichThuocBanhs.Find(id);
            if (kichThuocBanh == null)
            {
                return HttpNotFound();
            }
            return View(kichThuocBanh);
        }

        // POST: Admin/KichThuocBanhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKichThuocBanh,KichThuoc,GiaTien")] KichThuocBanh kichThuocBanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kichThuocBanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kichThuocBanh);
        }

        // GET: Admin/KichThuocBanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichThuocBanh kichThuocBanh = db.KichThuocBanhs.Find(id);
            if (kichThuocBanh == null)
            {
                return HttpNotFound();
            }
            return View(kichThuocBanh);
        }

        // POST: Admin/KichThuocBanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KichThuocBanh kichThuocBanh = db.KichThuocBanhs.Find(id);
            db.KichThuocBanhs.Remove(kichThuocBanh);
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