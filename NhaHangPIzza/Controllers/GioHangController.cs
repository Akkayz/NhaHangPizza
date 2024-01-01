using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class GioHangController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public ActionResult Index()
        {
            // Lấy thông tin giỏ hàng từ Session cho món ăn
            List<GioHang> gioHangMonAn = Session["GioHang"] as List<GioHang>;
            if (gioHangMonAn == null)
            {
                gioHangMonAn = new List<GioHang>();
            }
            ViewBag.GioHangMonAn = gioHangMonAn;

            // Lấy thông tin giỏ hàng từ Session cho nước uống
            List<GioHangNuocUong> gioHangNuocUong = Session["GioHangNuocUong"] as List<GioHangNuocUong>;
            if (gioHangNuocUong == null)
            {
                gioHangNuocUong = new List<GioHangNuocUong>();
            }
            ViewBag.GioHangNuocUong = gioHangNuocUong;

            List<GioHangCombo> gioHangCombo = Session["GioHangCombo"] as List<GioHangCombo>;
            if (gioHangCombo == null)
            {
                gioHangCombo = new List<GioHangCombo>();
            }
            List<BAN> danhSachBan = db.BANs.ToList();
            ViewBag.DanhSachBan = danhSachBan;
            ViewBag.GioHangCombo = gioHangCombo;
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        [HttpPost]
        public ActionResult ThemMonAn(int idMonAn, int iIDKichThuocBanh, int idVoBanh, int soLuong, string GiaTien)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
            }

            // Chuyển đổi chuỗi GiaTien sang decimal
            if (decimal.TryParse(GiaTien, out decimal giaTienDecimal))
            {
                // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                GioHang monAnTrongGioHang = gioHang.Find(item => item.iMaMonAn == idMonAn && item.iKichThuocBanh == iIDKichThuocBanh && item.iVoBanh == idVoBanh);
                if (monAnTrongGioHang == null)
                {
                    // Nếu chưa có, thêm sản phẩm mới vào giỏ hàng
                    GioHang monAnMoi = new GioHang(idMonAn, iIDKichThuocBanh, idVoBanh, soLuong, giaTienDecimal);
                    gioHang.Add(monAnMoi);
                }
                else
                {
                    // Nếu đã có, cập nhật số lượng
                    monAnTrongGioHang.iSoLuong += soLuong;
                }

                // Lưu thông tin giỏ hàng vào Session
                Session["GioHang"] = gioHang;

                // Chuyển hướng về trang Index
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult XoaMonAn(int idMonAn, int iIDKichThuocBanh, int idVoBanh)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
            }

            // Tìm sản phẩm cần xóa
            GioHang monAnCanXoa = gioHang.Find(item => item.iMaMonAn == idMonAn && item.iIDKichThuocBanh == iIDKichThuocBanh && item.iIdVoBanh == idVoBanh);
            if (monAnCanXoa != null)
            {
                // Nếu sản phẩm tồn tại, xóa khỏi giỏ hàng
                gioHang.Remove(monAnCanXoa);
            }

            // Lưu thông tin giỏ hàng vào Session
            Session["GioHang"] = gioHang;

            // Chuyển hướng về trang Index
            return RedirectToAction("Index");
        }

        // Phần NuocUong
        [HttpPost]
        public ActionResult ThemNuocUong(int idNuocUong, string soLuong)
        {
            // Chuyển đổi giá trị soLuong sang kiểu số nguyên
            if (int.TryParse(soLuong, out int soLuongInt))
            {
                // Lấy thông tin giỏ hàng từ Session
                List<GioHangNuocUong> gioHangNuocUong = Session["GioHangNuocUong"] as List<GioHangNuocUong>;
                if (gioHangNuocUong == null)
                {
                    gioHangNuocUong = new List<GioHangNuocUong>();
                }

                // Kiểm tra xem nước uống đã tồn tại trong giỏ hàng chưa
                GioHangNuocUong nuocUongTrongGioHang = gioHangNuocUong.Find(item => item.iMaNuocUong == idNuocUong);
                if (nuocUongTrongGioHang == null)
                {
                    // Nếu chưa có, thêm nước uống mới vào giỏ hàng nước uống
                    GioHangNuocUong nuocUongMoi = new GioHangNuocUong(idNuocUong, soLuongInt);
                    gioHangNuocUong.Add(nuocUongMoi);
                }
                else
                {
                    // Nếu đã có, cập nhật số lượng
                    nuocUongTrongGioHang.iSoLuong += soLuongInt;
                }

                // Lưu thông tin giỏ hàng nước uống vào Session
                Session["GioHangNuocUong"] = gioHangNuocUong;

                // Chuyển hướng về trang Index
                return RedirectToAction("Index");
            }
            else
            {
                // Xử lý trường hợp không thể chuyển đổi chuỗi sang số nguyên
                // Có thể thêm logic xử lý lỗi tùy ý
                return RedirectToAction("Index");
            }
        }

        public ActionResult XoaNuocUong(int idNuocUong)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHangNuocUong> gioHangNuocUong = Session["GioHangNuocUong"] as List<GioHangNuocUong>;
            if (gioHangNuocUong == null)
            {
                gioHangNuocUong = new List<GioHangNuocUong>();
            }

            // Tìm sản phẩm cần xóa
            GioHangNuocUong nuocUongCanXoa = gioHangNuocUong.Find(item => item.iMaNuocUong == idNuocUong);
            if (nuocUongCanXoa != null)
            {
                // Nếu sản phẩm tồn tại, xóa khỏi giỏ hàng
                gioHangNuocUong.Remove(nuocUongCanXoa);
            }

            // Lưu thông tin giỏ hàng vào Session
            Session["GioHang"] = gioHangNuocUong;

            // Chuyển hướng về trang Index
            return RedirectToAction("Index");
        }

        // Phần Combo
        [HttpPost]
        public ActionResult ThemCombo(int idCombo, string soLuong)
        {
            // Chuyển đổi giá trị soLuong sang kiểu số nguyên
            if (int.TryParse(soLuong, out int soLuongInt))
            {
                // Lấy thông tin giỏ hàng từ Session
                List<GioHangCombo> gioHangCombo = Session["GioHangCombo"] as List<GioHangCombo>;
                if (gioHangCombo == null)
                {
                    gioHangCombo = new List<GioHangCombo>();
                }

                // Kiểm tra xem nước uống đã tồn tại trong giỏ hàng chưa
                GioHangCombo comboTrongGioHang = gioHangCombo.Find(item => item.iMaCombo == idCombo);
                if (comboTrongGioHang == null)
                {
                    // Nếu chưa có, thêm nước uống mới vào giỏ hàng nước uống
                    GioHangCombo comboMoi = new GioHangCombo(idCombo, soLuongInt);
                    gioHangCombo.Add(comboMoi);
                }
                else
                {
                    // Nếu đã có, cập nhật số lượng
                    comboTrongGioHang.iSoLuong += soLuongInt;
                }

                // Lưu thông tin giỏ hàng nước uống vào Session
                Session["GioHangCombo"] = gioHangCombo;

                // Chuyển hướng về trang Index
                return RedirectToAction("Index");
            }
            else
            {
                // Xử lý trường hợp không thể chuyển đổi chuỗi sang số nguyên
                // Có thể thêm logic xử lý lỗi tùy ý
                return RedirectToAction("Index");
            }
        }

        public ActionResult XoaCombo(int idCombo)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHangCombo> gioHangCombo = Session["GioHangCombo"] as List<GioHangCombo>;
            if (gioHangCombo == null)
            {
                gioHangCombo = new List<GioHangCombo>();
            }

            // Tìm sản phẩm cần xóa
            GioHangCombo comboCanXoa = gioHangCombo.Find(item => item.iMaCombo == idCombo);
            if (comboCanXoa != null)
            {
                // Nếu sản phẩm tồn tại, xóa khỏi giỏ hàng
                gioHangCombo.Remove(comboCanXoa);
            }

            // Lưu thông tin giỏ hàng vào Session
            Session["GioHang"] = gioHangCombo;

            // Chuyển hướng về trang Index
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult XacNhanDatHang(int ddlBan)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            List<GioHangNuocUong> gioHangNuocUong = Session["GioHangNuocUong"] as List<GioHangNuocUong>;
            List<GioHangCombo> gioHangCombo = Session["GioHangCombo"] as List<GioHangCombo>;

            // Kiểm tra xem giỏ hàng có sản phẩm nào không
            if (gioHang != null || gioHangNuocUong != null || gioHangCombo != null)
            {
                // Tạo đối tượng HoaDon
                HoaDon hoaDon = new HoaDon
                {
                    TrangThai = "Đã đặt",
                    ThoiGianDatHang = DateTime.Now,
                    MaBan = ddlBan
                };

                // Lưu thông tin hóa đơn vào cơ sở dữ liệu
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();

                // Lưu thông tin chi tiết hóa đơn (món ăn, nước uống, combo) vào cơ sở dữ liệu
                if (gioHang != null)
                {
                    foreach (var item in gioHang)
                    {
                        ChiTietMonAn_HoaDon chiTietMonAn = new ChiTietMonAn_HoaDon
                        {
                            MaHD = hoaDon.MaHD,
                            MaMonAn = item.iMaMonAn,
                            SoLuong = item.iSoLuong,
                            IdVoBanh = item.iVoBanh,
                            IDKichThuocBanh = item.iKichThuocBanh,
                            GiaTien = (decimal?)item.dGiaTien + @item.giaTienKichThuocBanh + @item.giaTienVoBanh
                            // Các thông tin khác của chi tiết hóa đơn món ăn
                        };

                        db.ChiTietMonAn_HoaDon.Add(chiTietMonAn);
                        db.SaveChanges(); // Lưu để có thể lấy được Id
                    }
                }

                // Lưu thông tin chi tiết hóa đơn (nước uống) vào cơ sở dữ liệu
                if (gioHangNuocUong != null)
                {
                    foreach (var item in gioHangNuocUong)
                    {
                        ChiTietNuocUong_HoaDon chiTietNuocUong = new ChiTietNuocUong_HoaDon
                        {
                            MaHD = hoaDon.MaHD,
                            MaNuocUong = item.iMaNuocUong,
                            SoLuong = item.iSoLuong,
                            GiaTien = item.dGiaTien
                            // Các thông tin khác của chi tiết hóa đơn nước uống
                        };

                        db.ChiTietNuocUong_HoaDon.Add(chiTietNuocUong);
                        db.SaveChanges(); // Lưu để có thể lấy được Id
                    }
                }

                // Lưu thông tin chi tiết hóa đơn (combo) vào cơ sở dữ liệu
                if (gioHangCombo != null)
                {
                    foreach (var item in gioHangCombo)
                    {
                        ChiTietCombo chiTietCombo = new ChiTietCombo
                        {
                            MaHD = hoaDon.MaHD,
                            MaComBo = item.iMaCombo,
                            SoLuong = item.iSoLuong,
                            GiaTien = item.dGiaTien
                            // Các thông tin khác của chi tiết hóa đơn combo
                        };

                        db.ChiTietComboes.Add(chiTietCombo);
                        db.SaveChanges(); // Lưu để có thể lấy được Id
                    }
                }

                // Tính tổng tiền của hóa đơn và cập nhật vào cơ sở dữ liệu
                hoaDon.TongTien = CalculateTotalAmount(hoaDon.MaHD);
                db.SaveChanges();

                // Xóa thông tin giỏ hàng từ Session
                Session["GioHang"] = null;
                Session["GioHangNuocUong"] = null;
                Session["GioHangCombo"] = null;

                TempData["ThongBao"] = "Đặt hàng thành công!";
                // Chuyển hướng về trang Index hoặc trang cảm ơn
                return RedirectToAction("ChiTietHoaDon", new { maHoaDon = hoaDon.MaHD });
            }
            else
            {
                TempData["ThongBao"] = "Giỏ hàng trống!";
                // Giỏ hàng trống, xử lý tùy ý (ví dụ: hiển thị thông báo)
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChiTietHoaDon(int maHoaDon)
        {
            // Lấy thông tin hóa đơn từ cơ sở dữ liệu
            var hoaDon = db.HoaDons.Find(maHoaDon);

            if (hoaDon != null && hoaDon.TrangThai == "Đã thanh toán")
            {
                // Nếu trạng thái là "Đã thanh toán", chuyển hướng về trang chủ
                return RedirectToAction("Index", "Home");
            }

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

        // Hàm tính tổng tiền của hóa đơn
        private decimal CalculateTotalAmount(int maHoaDon)
        {
            decimal totalAmount = 0;

            // Tính tổng tiền từ chi tiết hóa đơn (món ăn)
            var chiTietMonAnList = db.ChiTietMonAn_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            foreach (var chiTietMonAn in chiTietMonAnList)
            {
                totalAmount += (chiTietMonAn.GiaTien ?? 0) * chiTietMonAn.SoLuong ?? 1;
            }

            // Tính tổng tiền từ chi tiết hóa đơn (nước uống)
            var chiTietNuocUongList = db.ChiTietNuocUong_HoaDon.Where(ct => ct.MaHD == maHoaDon).ToList();
            foreach (var chiTietNuocUong in chiTietNuocUongList)
            {
                totalAmount += (chiTietNuocUong.GiaTien ?? 0) * chiTietNuocUong.SoLuong ?? 1;
            }

            // Tính tổng tiền từ chi tiết hóa đơn (combo)
            var chiTietComboList = db.ChiTietComboes.Where(ct => ct.MaHD == maHoaDon).ToList();
            foreach (var chiTietCombo in chiTietComboList)
            {
                totalAmount += (chiTietCombo.GiaTien ?? 0) * chiTietCombo.SoLuong ?? 1;
            }

            return totalAmount;
        }
    }
}