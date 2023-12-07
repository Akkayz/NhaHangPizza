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
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
            }

            return View(gioHang);
        }

        [HttpPost]
        public ActionResult ThemMonAn(int idMonAn, int iIDKichThuocBanh, int idVoBanh, int soLuong)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            GioHang monAnTrongGioHang = gioHang.FirstOrDefault(item => item.iMaMonAn == idMonAn && item.iIDKichThuocBanh == iIDKichThuocBanh && item.iIdVoBanh == idVoBanh);
            if (monAnTrongGioHang == null)
            {
                // Nếu chưa có, thêm sản phẩm mới vào giỏ hàng
                GioHang monAnMoi = new GioHang(idMonAn, iIDKichThuocBanh, idVoBanh, soLuong);
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

        public ActionResult XoaMonAn(int idMonAn, int iIDKichThuocBanh, int idVoBanh)
        {
            // Lấy thông tin giỏ hàng từ Session
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
            }

            // Tìm sản phẩm cần xóa
            GioHang monAnCanXoa = gioHang.FirstOrDefault(item => item.iMaMonAn == idMonAn && item.iIDKichThuocBanh == iIDKichThuocBanh && item.iIdVoBanh == idVoBanh);
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
    }
}