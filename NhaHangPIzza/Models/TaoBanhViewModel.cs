using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhaHangPIzza.Models
{
    public class TaoBanhViewModel
    {
        public string TenBanh { get; set; }
        public decimal GiaTien { get; set; }
        public int IdLoaiBanh { get; set; }
        public List<int> SelectedThanhPhans { get; set; }
        public int IdVoBanh { get; set; }
        public int IdKichThuocBanh { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> Maban { get; set; }
        public Nullable<bool> TheLoai { get; set; }
    }
}