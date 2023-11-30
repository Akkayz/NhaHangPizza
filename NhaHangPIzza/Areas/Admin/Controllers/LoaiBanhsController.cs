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
    public class LoaiBanhsController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/LoaiBanhs
        public ActionResult Index()
        {
            return View(db.LoaiBanhs.ToList());
        }

        // GET: Admin/LoaiBanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBanh loaiBanh = db.LoaiBanhs.Find(id);
            if (loaiBanh == null)
            {
                return HttpNotFound();
            }
            return View(loaiBanh);
        }

        // GET: Admin/LoaiBanhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiBanhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiBanh,TenLoaiBanh")] LoaiBanh loaiBanh)
        {
            if (ModelState.IsValid)
            {
                db.LoaiBanhs.Add(loaiBanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiBanh);
        }

        // GET: Admin/LoaiBanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBanh loaiBanh = db.LoaiBanhs.Find(id);
            if (loaiBanh == null)
            {
                return HttpNotFound();
            }
            return View(loaiBanh);
        }

        // POST: Admin/LoaiBanhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiBanh,TenLoaiBanh")] LoaiBanh loaiBanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiBanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiBanh);
        }

        // GET: Admin/LoaiBanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiBanh loaiBanh = db.LoaiBanhs.Find(id);
            if (loaiBanh == null)
            {
                return HttpNotFound();
            }
            return View(loaiBanh);
        }

        // POST: Admin/LoaiBanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiBanh loaiBanh = db.LoaiBanhs.Find(id);
            db.LoaiBanhs.Remove(loaiBanh);
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