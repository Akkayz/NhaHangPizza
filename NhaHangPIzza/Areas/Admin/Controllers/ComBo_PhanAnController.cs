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
    public class ComBo_PhanAnController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/ComBo_PhanAn
        public ActionResult Index()
        {
            var comBo_PhanAn = db.ComBo_PhanAn.Include(c => c.Combo).Include(c => c.MonAn);
            return View(comBo_PhanAn.ToList());
        }

        // GET: Admin/ComBo_PhanAn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanAn comBo_PhanAn = db.ComBo_PhanAn.Find(id);
            if (comBo_PhanAn == null)
            {
                return HttpNotFound();
            }
            return View(comBo_PhanAn);
        }

        // GET: Admin/ComBo_PhanAn/Create
        public ActionResult Create()
        {
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo");
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn");
            return View();
        }

        // POST: Admin/ComBo_PhanAn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCombo,MaMonAn,SoLuong,ID")] ComBo_PhanAn comBo_PhanAn)
        {
            if (ModelState.IsValid)
            {
                db.ComBo_PhanAn.Add(comBo_PhanAn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanAn.MaCombo);
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn", comBo_PhanAn.MaMonAn);
            return View(comBo_PhanAn);
        }

        // GET: Admin/ComBo_PhanAn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanAn comBo_PhanAn = db.ComBo_PhanAn.Find(id);
            if (comBo_PhanAn == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanAn.MaCombo);
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn", comBo_PhanAn.MaMonAn);
            return View(comBo_PhanAn);
        }

        // POST: Admin/ComBo_PhanAn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCombo,MaMonAn,SoLuong,ID")] ComBo_PhanAn comBo_PhanAn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comBo_PhanAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanAn.MaCombo);
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn", comBo_PhanAn.MaMonAn);
            return View(comBo_PhanAn);
        }

        // GET: Admin/ComBo_PhanAn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanAn comBo_PhanAn = db.ComBo_PhanAn.Find(id);
            if (comBo_PhanAn == null)
            {
                return HttpNotFound();
            }
            return View(comBo_PhanAn);
        }

        // POST: Admin/ComBo_PhanAn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComBo_PhanAn comBo_PhanAn = db.ComBo_PhanAn.Find(id);
            db.ComBo_PhanAn.Remove(comBo_PhanAn);
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