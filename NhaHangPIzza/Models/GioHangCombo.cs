using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhaHangPIzza.Models;

namespace NhaHangPIzza.Models
{
    public class GioHangCombo
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public int iMaCombo { get; set; }
        public string sTenCombo { get; set; }
        public string sHinhAnh { get; set; }
        public decimal? dGiaTien { get; set; }
        public int iSoLuong { get; set; }

        public double dThanhTien
        {
            get { return iSoLuong * (double)dGiaTien; }
        }

        public GioHangCombo(int idCombo, int soLuong)
        {
            iMaCombo = idCombo;

            Combo cb = db.Comboes.Single(c => c.MaCombo == iMaCombo);
            sTenCombo = cb.TenCombo;
            sHinhAnh = cb.HinhAnh;
            dGiaTien = cb.GiaTien;
            iSoLuong = soLuong;
        }
    }
}