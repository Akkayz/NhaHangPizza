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
    public class MonAnsController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/MonAns
        public ActionResult Index()
        {
            var monAns = db.MonAns.Include(m => m.LoaiBanh);
            return View(monAns.ToList());
        }

        // GET: Admin/MonAns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // GET: Admin/MonAns/Create
        public ActionResult Create()
        {
            ViewBag.IDLoaiBanh = new SelectList(db.LoaiBanhs, "IDLoaiBanh", "TenLoaiBanh");
            return View();
        }

        // POST: Admin/MonAns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMonAn,TenMonAn,GiaTien,IDLoaiBanh,X")] MonAn monAn, HttpPostedFileBase HinhAnhUpload)
        {
            if (ModelState.IsValid)
            {
                // Xử lý tải ảnh lên và lưu tên file vào cơ sở dữ liệu
                if (HinhAnhUpload != null && HinhAnhUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnhUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/BanhPizza/"), fileName);
                    HinhAnhUpload.SaveAs(path);

                    // Lưu tên file vào thuộc tính HinhAnh của đối tượng MonAn
                    monAn.HinhAnh = fileName;
                }

                db.MonAns.Add(monAn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLoaiBanh = new SelectList(db.LoaiBanhs, "IDLoaiBanh", "TenLoaiBanh", monAn.IDLoaiBanh);
            return View(monAn);
        }

        // GET: Admin/MonAns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLoaiBanh = new SelectList(db.LoaiBanhs, "IDLoaiBanh", "TenLoaiBanh", monAn.IDLoaiBanh);
            return View(monAn);
        }

        // POST: Admin/MonAns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMonAn,TenMonAn,GiaTien,IDLoaiBanh,X,HinhAnh")] MonAn monAn, HttpPostedFileBase HinhAnhUpload)
        {
            if (ModelState.IsValid)
            {
                // Xử lý tải ảnh lên và lưu tên file vào cơ sở dữ liệu
                if (HinhAnhUpload != null && HinhAnhUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnhUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/BanhPizza/"), fileName);
                    HinhAnhUpload.SaveAs(path);

                    // Lưu tên file vào thuộc tính HinhAnh của đối tượng MonAn
                    monAn.HinhAnh = fileName;
                }

                db.Entry(monAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLoaiBanh = new SelectList(db.LoaiBanhs, "IDLoaiBanh", "TenLoaiBanh", monAn.IDLoaiBanh);
            return View(monAn);
        }

        // GET: Admin/MonAns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }
            return View(monAn);
        }

        // POST: Admin/MonAns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MonAn monAn = db.MonAns.Find(id);
            db.MonAns.Remove(monAn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/MonAns/XemThanhPhanBanh/1
        public ActionResult XemThanhPhanBanh(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MonAn monAn = db.MonAns.Find(id);
            if (monAn == null)
            {
                return HttpNotFound();
            }

            // Lấy thông tin về thành phần bánh (ví dụ: danh sách thành phần bánh thuộc món ăn)
            var thanhPhanBanh = db.MonAn_ThanhPhanBanh
                .Where(m => m.MaMonAn == id)
                .Select(m => m.THANHPHANBANH)
                .ToList();

            ViewBag.MonAn = monAn;
            ViewBag.ThanhPhanBanh = thanhPhanBanh;

            return View();
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