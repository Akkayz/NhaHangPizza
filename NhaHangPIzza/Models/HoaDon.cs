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
    
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            this.ChiTietMonAn_HoaDon = new HashSet<ChiTietMonAn_HoaDon>();
        }
    
        public int MaHD { get; set; }
        public string TrangThai { get; set; }
        public System.DateTime ThoiGianThanhToan { get; set; }
        public int MaBan { get; set; }
        public int IdChiTietMonAn { get; set; }
        public Nullable<int> IdChiTietNuocUong { get; set; }
        public Nullable<System.DateTime> ThoiGianDatHang { get; set; }
        public Nullable<int> IDChiTietComBo { get; set; }
        public Nullable<decimal> TongTien { get; set; }
    
        public virtual BAN BAN { get; set; }
        public virtual ChiTietCombo ChiTietCombo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietMonAn_HoaDon> ChiTietMonAn_HoaDon { get; set; }
        public virtual ChiTietNuocUong_HoaDon ChiTietNuocUong_HoaDon { get; set; }
    }
}
