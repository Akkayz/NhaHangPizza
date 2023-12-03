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
    public class ComBo_PhanUongController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/ComBo_PhanUong
        public ActionResult Index()
        {
            var comBo_PhanUong = db.ComBo_PhanUong.Include(c => c.Combo).Include(c => c.NuocUong);
            return View(comBo_PhanUong.ToList());
        }

        // GET: Admin/ComBo_PhanUong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanUong comBo_PhanUong = db.ComBo_PhanUong.Find(id);
            if (comBo_PhanUong == null)
            {
                return HttpNotFound();
            }
            return View(comBo_PhanUong);
        }

        // GET: Admin/ComBo_PhanUong/Create
        public ActionResult Create()
        {
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo");
            ViewBag.MaNuocUong = new SelectList(db.NuocUongs, "MaNuocUong", "TenNuocUong");
            return View();
        }

        // POST: Admin/ComBo_PhanUong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaCombo,MaNuocUong,SoLuong")] ComBo_PhanUong comBo_PhanUong)
        {
            if (ModelState.IsValid)
            {
                db.ComBo_PhanUong.Add(comBo_PhanUong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanUong.MaCombo);
            ViewBag.MaNuocUong = new SelectList(db.NuocUongs, "MaNuocUong", "TenNuocUong", comBo_PhanUong.MaNuocUong);
            return View(comBo_PhanUong);
        }

        // GET: Admin/ComBo_PhanUong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanUong comBo_PhanUong = db.ComBo_PhanUong.Find(id);
            if (comBo_PhanUong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanUong.MaCombo);
            ViewBag.MaNuocUong = new SelectList(db.NuocUongs, "MaNuocUong", "TenNuocUong", comBo_PhanUong.MaNuocUong);
            return View(comBo_PhanUong);
        }

        // POST: Admin/ComBo_PhanUong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaCombo,MaNuocUong,SoLuong")] ComBo_PhanUong comBo_PhanUong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comBo_PhanUong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCombo = new SelectList(db.Comboes, "MaCombo", "TenCombo", comBo_PhanUong.MaCombo);
            ViewBag.MaNuocUong = new SelectList(db.NuocUongs, "MaNuocUong", "TenNuocUong", comBo_PhanUong.MaNuocUong);
            return View(comBo_PhanUong);
        }

        // GET: Admin/ComBo_PhanUong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComBo_PhanUong comBo_PhanUong = db.ComBo_PhanUong.Find(id);
            if (comBo_PhanUong == null)
            {
                return HttpNotFound();
            }
            return View(comBo_PhanUong);
        }

        // POST: Admin/ComBo_PhanUong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComBo_PhanUong comBo_PhanUong = db.ComBo_PhanUong.Find(id);
            db.ComBo_PhanUong.Remove(comBo_PhanUong);
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