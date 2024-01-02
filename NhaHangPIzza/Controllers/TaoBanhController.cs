using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class TaoBanhController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // ... (các action khác)

        [HttpPost]
        public ActionResult TaoBanh(TaoBanhViewModel model, int ddlBan)
        {
            if (ModelState.IsValid)
            {
                // Tạo đối tượng MonAn từ thông tin trong model
                var monAn = new MonAn
                {
                    TenMonAn = "Bánh pizza mặc định",
                    GiaTien = decimal.Parse("120000"),
                    TheLoai = false,
                    HinhAnh = "Anhmacdinh.jpg",
                    Maban = ddlBan,
                    // ... (thêm các thuộc tính khác)
                };

                // Lưu đối tượng MonAn vào cơ sở dữ liệu
                db.MonAns.Add(monAn);
                db.SaveChanges();

                // Chuyển hướng đến action mới để chọn thành phần bánh
                return RedirectToAction("Create", new { maMonAn = monAn.MaMonAn });
            }

            // Nếu ModelState không hợp lệ, hiển thị lại trang với thông báo lỗi
            return View(model);
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

                return RedirectToAction("Index", "BanhPizza");
            }

            ViewBag.ThanhPhanList = db.THANHPHANBANHs.Select(t => new SelectListItem
            {
                Value = t.IdThanhPhan.ToString(),
                Text = t.TenThanhPhan
            });

            return RedirectToAction("Index", "BanhPizza");
        }
    }
}