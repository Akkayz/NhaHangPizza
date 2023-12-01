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
    public class MonAn_ThanhPhanBanhController : Controller
    {
        private QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/MonAn_ThanhPhanBanh
        public ActionResult Index(int? maMonAn)
        {
            if (maMonAn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh
                .Include(m => m.MonAn)
                .Include(m => m.THANHPHANBANH)
                .Where(m => m.MaMonAn == maMonAn)
                .ToList();

            ViewBag.TenMonAn = db.MonAns.Where(m => m.MaMonAn == maMonAn).Select(m => m.TenMonAn).FirstOrDefault();
            ViewBag.MaMonAn = maMonAn; // Thêm dòng này để truyền giá trị MaMonAn vào view

            return View(monAn_ThanhPhanBanh);
        }

        // GET: Admin/MonAn_ThanhPhanBanh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn_ThanhPhanBanh monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh.Find(id);
            if (monAn_ThanhPhanBanh == null)
            {
                return HttpNotFound();
            }
            return View(monAn_ThanhPhanBanh);
        }

        [HttpGet]
        public ActionResult Create(int? maMonAn)
        {
            if (maMonAn == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy thông tin Mã và Tên Món Ăn từ database
            var monAnInfo = db.MonAns.Find(maMonAn);
            ViewBag.TenMonAn = monAnInfo?.TenMonAn;
            ViewBag.MaMonAn = maMonAn;

            // Khởi tạo đối tượng MonAn_ThanhPhanBanh với MaMonAn
            var model = new MonAn_ThanhPhanBanh
            {
                MaMonAn = maMonAn
            };

            // Lấy danh sách IdThanhPhan từ database và đặt vào ViewBag.IdThanhPhan
            ViewBag.IdThanhPhan = new SelectList(db.THANHPHANBANHs, "IdThanhPhan", "TenThanhPhan");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMonAn,IdThanhPhan,ID")] MonAn_ThanhPhanBanh monAn_ThanhPhanBanh)
        {
            if (ModelState.IsValid)
            {
                db.MonAn_ThanhPhanBanh.Add(monAn_ThanhPhanBanh);
                db.SaveChanges();
                return RedirectToAction("Index", new { maMonAn = monAn_ThanhPhanBanh.MaMonAn });
            }

            // Lấy danh sách IdThanhPhan từ database và đặt vào ViewBag.IdThanhPhan
            ViewBag.IdThanhPhan = new SelectList(db.THANHPHANBANHs, "IdThanhPhan", "TenThanhPhan", monAn_ThanhPhanBanh.IdThanhPhan);

            return View(monAn_ThanhPhanBanh);
        }

        // GET: Admin/MonAn_ThanhPhanBanh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn_ThanhPhanBanh monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh.Find(id);
            if (monAn_ThanhPhanBanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn", monAn_ThanhPhanBanh.MaMonAn);
            ViewBag.IdThanhPhan = new SelectList(db.THANHPHANBANHs, "IdThanhPhan", "TenThanhPhan", monAn_ThanhPhanBanh.IdThanhPhan);
            return View(monAn_ThanhPhanBanh);
        }

        // POST: Admin/MonAn_ThanhPhanBanh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMonAn,IdThanhPhan,ID")] MonAn_ThanhPhanBanh monAn_ThanhPhanBanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monAn_ThanhPhanBanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMonAn = new SelectList(db.MonAns, "MaMonAn", "TenMonAn", monAn_ThanhPhanBanh.MaMonAn);
            ViewBag.IdThanhPhan = new SelectList(db.THANHPHANBANHs, "IdThanhPhan", "TenThanhPhan", monAn_ThanhPhanBanh.IdThanhPhan);
            return View(monAn_ThanhPhanBanh);
        }

        // GET: Admin/MonAn_ThanhPhanBanh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn_ThanhPhanBanh monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh.Find(id);
            if (monAn_ThanhPhanBanh == null)
            {
                return HttpNotFound();
            }
            return View(monAn_ThanhPhanBanh);
        }

        // POST: Admin/MonAn_ThanhPhanBanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonAn_ThanhPhanBanh monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh.Find(id);
            db.MonAn_ThanhPhanBanh.Remove(monAn_ThanhPhanBanh);
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