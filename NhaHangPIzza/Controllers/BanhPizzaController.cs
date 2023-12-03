using NhaHangPIzza.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class BanhPizzaController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public ActionResult Index()
        {
            // Lấy danh sách loại bánh
            List<LoaiBanh> danhSachLoaiBanh = db.LoaiBanhs.ToList();

            // Lấy danh sách món ăn theo từng loại bánh
            Dictionary<LoaiBanh, List<MonAn>> danhSachMonAnTheoLoai = new Dictionary<LoaiBanh, List<MonAn>>();

            foreach (var loaiBanh in danhSachLoaiBanh)
            {
                var monAnTheoLoai = db.MonAns.Include("MonAn_ThanhPhanBanh.THANHPHANBANH")
                                              .Where(ma => ma.IDLoaiBanh == loaiBanh.IDLoaiBanh)
                                              .ToList();

                danhSachMonAnTheoLoai.Add(loaiBanh, monAnTheoLoai);
            }

            ViewBag.DanhSachKichThuoc = db.KichThuocBanhs.ToList();
            ViewBag.DanhSachVoBanh = db.VOBANHs.ToList();
            ViewBag.DanhSachLoaiBanh = danhSachLoaiBanh;
            ViewBag.DanhSachMonAnTheoLoai = danhSachMonAnTheoLoai;

            return View(danhSachLoaiBanh);
        }
    }
}