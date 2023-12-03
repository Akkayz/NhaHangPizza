using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhaHangPIzza.Models;

namespace NhaHangPIzza.Areas.Admin.Controllers
{
    public class NuocUongsController : Controller
    {
        private QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/NuocUongs
        public ActionResult Index()
        {
            return View(db.NuocUongs.ToList());
        }

        // GET: Admin/NuocUongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocUong nuocUong = db.NuocUongs.Find(id);
            if (nuocUong == null)
            {
                return HttpNotFound();
            }
            return View(nuocUong);
        }

        // GET: Admin/NuocUongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NuocUongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNuocUong,TenNuocUong,GiaTien,HinhAnh")] NuocUong nuocUong, HttpPostedFileBase HinhAnhUpload)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnhUpload != null && HinhAnhUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnhUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/NuocGiaiKhat/"), fileName);
                    HinhAnhUpload.SaveAs(path);

                    // Lưu tên file vào thuộc tính HinhAnh của đối tượng MonAn
                    nuocUong.HinhAnh = fileName;
                }
                db.NuocUongs.Add(nuocUong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nuocUong);
        }

        // GET: Admin/NuocUongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocUong nuocUong = db.NuocUongs.Find(id);
            if (nuocUong == null)
            {
                return HttpNotFound();
            }
            return View(nuocUong);
        }

        // POST: Admin/NuocUongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNuocUong,TenNuocUong,GiaTien,HinhAnh")] NuocUong nuocUong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nuocUong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nuocUong);
        }

        // GET: Admin/NuocUongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NuocUong nuocUong = db.NuocUongs.Find(id);
            if (nuocUong == null)
            {
                return HttpNotFound();
            }
            return View(nuocUong);
        }

        // POST: Admin/NuocUongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NuocUong nuocUong = db.NuocUongs.Find(id);
            db.NuocUongs.Remove(nuocUong);
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