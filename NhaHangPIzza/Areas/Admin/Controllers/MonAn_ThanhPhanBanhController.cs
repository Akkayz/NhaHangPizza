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
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

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

            // Lấy danh sách IdThanhPhan từ database và đặt vào ViewBag.ThanhPhanList
            ViewBag.ThanhPhanList = db.THANHPHANBANHs.Select(t => new SelectListItem
            {
                Value = t.IdThanhPhan.ToString(),
                Text = t.TenThanhPhan
            });

            // Khởi tạo đối tượng MonAn_ThanhPhanBanh với MaMonAn và SelectedThanhPhans là một List rỗng
            var model = new MonAn_ThanhPhanBanh
            {
                MaMonAn = maMonAn,
                SelectedThanhPhans = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonAn_ThanhPhanBanh monAn_ThanhPhanBanh)
        {
            if (ModelState.IsValid)
            {
                // Bỏ qua giữa nếu SelectedThanhPhans là null hoặc không có giá trị
                if (monAn_ThanhPhanBanh.SelectedThanhPhans != null && monAn_ThanhPhanBanh.SelectedThanhPhans.Any())
                {
                    foreach (var thanhPhanId in monAn_ThanhPhanBanh.SelectedThanhPhans)
                    {
                        var monAn_ThanhPhan = new MonAn_ThanhPhanBanh
                        {
                            MaMonAn = monAn_ThanhPhanBanh.MaMonAn,
                            IdThanhPhan = thanhPhanId
                        };

                        db.MonAn_ThanhPhanBanh.Add(monAn_ThanhPhan);
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { maMonAn = monAn_ThanhPhanBanh.MaMonAn });
            }

            ViewBag.ThanhPhanList = db.THANHPHANBANHs.Select(t => new SelectListItem
            {
                Value = t.IdThanhPhan.ToString(),
                Text = t.TenThanhPhan
            });

            return View(monAn_ThanhPhanBanh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteThanhPhan(int maMonAn, int idThanhPhan)
        {
            // Tìm và xoá thành phần từ database
            var monAn_ThanhPhanBanh = db.MonAn_ThanhPhanBanh
                .Where(m => m.MaMonAn == maMonAn && m.IdThanhPhan == idThanhPhan)
                .FirstOrDefault();

            if (monAn_ThanhPhanBanh != null)
            {
                db.MonAn_ThanhPhanBanh.Remove(monAn_ThanhPhanBanh);
                db.SaveChanges();
            }

            // Chuyển hướng về trang Index
            return RedirectToAction("Index", new { maMonAn });
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