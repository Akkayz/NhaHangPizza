using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Areas.NhanVien.Controllers
{
    public class DauBepController : Controller
    {
        // GET: NhanVien/DauBep
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public ActionResult Index()
        {
            // Truy vấn danh sách hóa đơn
            var danhSachHoaDon = db.HoaDons.Where(hd => hd.TrangThai != "Đã làm xong" && hd.TrangThai != "Đã thanh toán").ToList();

            return View(danhSachHoaDon);
        }

        public ActionResult ChiTietHoaDon(int maHoaDon)
        {
            var hoaDon = db.HoaDons.Find(maHoaDon);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            var chiTietMonAnList = db.ChiTietMonAn_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            var chiTietNuocUongList = db.ChiTietNuocUong_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            var chiTietComboList = db.ChiTietComboes.Where(ct => ct.MaHD == maHoaDon).ToList();

            ViewBag.HoaDon = hoaDon;
            ViewBag.ChiTietMonAnList = chiTietMonAnList;
            ViewBag.ChiTietNuocUongList = chiTietNuocUongList;
            ViewBag.ChiTietComboList = chiTietComboList;

            return View(hoaDon);
        }

        [HttpPost]
        public ActionResult CapNhatTrangThai(int maHoaDon, string trangThaiDauBep)
        {
            var hoaDon = db.HoaDons.Find(maHoaDon);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }

            hoaDon.TrangThai = trangThaiDauBep;
            db.SaveChanges();

            // Chuyển hướng hoặc trả về một ActionResult khác nếu cần
            return RedirectToAction("Index", "DauBep");
        }
    }
}