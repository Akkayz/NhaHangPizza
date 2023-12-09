﻿using NhaHangPIzza.Models;
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

            // Lưu thông tin giỏ hàng món ăn vào ViewBag
            ViewBag.GioHangMonAn = gioHangMonAn;

            // Lấy thông tin giỏ hàng từ Session cho nước uống
            List<GioHangNuocUong> gioHangNuocUong = Session["GioHangNuocUong"] as List<GioHangNuocUong>;
            if (gioHangNuocUong == null)
            {
                gioHangNuocUong = new List<GioHangNuocUong>();
            }

            // Lưu thông tin giỏ hàng nước uống vào ViewBag
            ViewBag.GioHangNuocUong = gioHangNuocUong;

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
    }
}