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
    
    public partial class MonAn_ThanhPhanBanh
    {
        public Nullable<int> MaMonAn { get; set; }
        public Nullable<int> IdThanhPhan { get; set; }
        public int ID { get; set; }
    
        public virtual MonAn MonAn { get; set; }
        public virtual THANHPHANBANH THANHPHANBANH { get; set; }
    }
}