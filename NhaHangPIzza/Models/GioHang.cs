using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace NhaHangPIzza.Models
{
    public class GioHang
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();
        public int iMaMonAn { get; set; }
        public string sTenMonAn { get; set; }

        public string sHinhAnh { get; set; }
        public decimal dGiaTien { get; set; }

        public int iIDLoaiBanh { get; set; }
        public string sLoaiBanh { get; set; }

        public int iVoBanh { get; set; }
        public int iIdVoBanh { get; set; }
        public string sDoDay { get; set; }

        public int iKichThuocBanh { get; set; }
        public int iIDKichThuocBanh { get; set; }
        public string sKichThuoc { get; set; }

        public int iSoLuong { get; set; }

        public double dThanhTien
        {
            get { return iSoLuong * (int)dGiaTien; }
        }

        public GioHang(int idMonAn, int iIDKichThuocBanh, int idVoBanh, int soLuong, decimal GiaTien)
        {
            iMaMonAn = idMonAn;

            MonAn ma = db.MonAns.Single(n => n.MaMonAn == iMaMonAn);
            sTenMonAn = ma.TenMonAn;
            sHinhAnh = ma.HinhAnh;
            sLoaiBanh = ma.LoaiBanh.TenLoaiBanh;

            iVoBanh = idVoBanh;
            VOBANH vb = db.VOBANHs.Single(v => v.IdVoBanh == iVoBanh);
            sDoDay = vb.DoDay;

            iKichThuocBanh = iIDKichThuocBanh;
            KichThuocBanh kt = db.KichThuocBanhs.Single(k => k.IDKichThuocBanh == iKichThuocBanh);
            sKichThuoc = kt.KichThuoc;
            dGiaTien = GiaTien;
            iSoLuong = soLuong;
        }
    }
}