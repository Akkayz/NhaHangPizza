//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NhaHangPIzza.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietMonAn_HoaDon
    {
        public int IdChiTietMonAn { get; set; }
        public int MaMonAn { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> MaHD { get; set; }
        public Nullable<int> IdVoBanh { get; set; }
        public Nullable<int> IDKichThuocBanh { get; set; }
        public Nullable<decimal> GiaTien { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
        public virtual KichThuocBanh KichThuocBanh { get; set; }
        public virtual VOBANH VOBANH { get; set; }
        public virtual MonAn MonAn { get; set; }
    }
}
