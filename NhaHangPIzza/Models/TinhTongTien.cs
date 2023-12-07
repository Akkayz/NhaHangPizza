using System;
using System.Globalization;
using System.Linq;

namespace NhaHangPIzza.Models
{
    public static class TinhTongTien
    {
        public static string TinhTongGiaTienFormatted(this MonAn monAn)
        {
            decimal tongGiaTien = monAn.GiaTien;

            if (monAn.MonAn_ThanhPhanBanh != null)
            {
                tongGiaTien += monAn.MonAn_ThanhPhanBanh
                    .Where(tpb => tpb.THANHPHANBANH != null)
                    .Sum(tpb => tpb.THANHPHANBANH.Giatien);
            }

            return tongGiaTien.ToString("N0", CultureInfo.CurrentCulture);
        }
    }
}