using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: HoaDon
        public ActionResult Index()
        {
            // Lấy danh sách bàn từ cơ sở dữ liệu
            List<BAN> danhSachBan = db.BANs.ToList();

            // Đặt danh sách bàn vào ViewBag để sử dụng trong view
            ViewBag.DanhSachBan = danhSachBan;

            return View();
        }
    }
}