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
            var danhSachHoaDon = db.HoaDons.ToList();

            return View(danhSachHoaDon);
        }

        public ActionResult ChiTietHoaDon(int maHoaDon)
        {
            // Lấy thông tin hóa đơn từ cơ sở dữ liệu
            var hoaDon = db.HoaDons.Find(maHoaDon);
            // Lấy thông tin chi tiết hóa đơn từ cơ sở dữ liệu
            var chiTietMonAnList = db.ChiTietMonAn_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            var chiTietNuocUongList = db.ChiTietNuocUong_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            var chiTietComboList = db.ChiTietComboes.Where(ct => ct.MaHD == maHoaDon).ToList();

            ViewBag.HoaDon = hoaDon;
            ViewBag.ChiTietMonAnList = chiTietMonAnList;
            ViewBag.ChiTietNuocUongList = chiTietNuocUongList;
            ViewBag.ChiTietComboList = chiTietComboList;

            return View();
        }
    }
}