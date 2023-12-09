using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhaHangPIzza.Models;

namespace NhaHangPIzza.Models
{
    public class GioHangNuocUong
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public int iMaNuocUong { get; set; }
        public string sTenNuocUong { get; set; }
        public string sHinhAnh { get; set; }
        public decimal dGiaTien { get; set; }
        public int iSoLuong { get; set; }

        public double dThanhTien
        {
            get { return iSoLuong * (double)dGiaTien; }
        }

        public GioHangNuocUong(int idNuocUong, int soLuong)
        {
            iMaNuocUong = idNuocUong;
            NuocUong nu = db.NuocUongs.Single(n => n.MaNuocUong == iMaNuocUong);
            sTenNuocUong = nu.TenNuocUong;
            sHinhAnh = nu.HinhAnh;
            dGiaTien = nu.GiaTien;
            iSoLuong = soLuong;
        }
    }
}