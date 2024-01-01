using NhaHangPIzza.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class BanhPizzaKhachHangLamController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public ActionResult Index(int? selectedBan)
        {
            // Truy vấn bánh pizza khách hàng làm
            var danhSachBanhPizza = db.MonAns
                .Where(m => m.TheLoai == false) // Lọc theo TheLoai = false
                .ToList();

            // Kiểm tra nếu có mã bàn được chọn, thêm điều kiện lọc theo mã bàn
            if (selectedBan.HasValue)
            {
                danhSachBanhPizza = danhSachBanhPizza
                    .Where(m => m.Maban == selectedBan.Value)
                    .ToList();
            }
            else
            {
                // Truy vấn các hóa đơn đã thanh toán
                var hoaDonDaThanhToan = db.HoaDons
                    .Where(hd => hd.TrangThai == "Đã thanh toán")
                    .SelectMany(hd => hd.ChiTietMonAn_HoaDon.Select(ct => ct.MaMonAn))
                    .ToList();

                // Loại bỏ bánh pizza có mã nằm trong ChiTietMonAn_HoaDon của hóa đơn đã thanh toán
                danhSachBanhPizza = danhSachBanhPizza.Where(m => !hoaDonDaThanhToan.Contains(m.MaMonAn)).ToList();
            }

            List<BAN> danhSachBan = db.BANs.ToList();
            ViewBag.DanhSachBan = danhSachBan;
            ViewBag.DanhSachKichThuoc = db.KichThuocBanhs.ToList();
            ViewBag.DanhSachVoBanh = db.VOBANHs.ToList();

            // Truyền mã bàn đã chọn vào ViewBag
            ViewBag.SelectedBan = selectedBan;

            return View(danhSachBanhPizza);
        }
    }
}