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
    
    public partial class ComBo_PhanUong
    {
        public int ID { get; set; }
        public Nullable<int> MaCombo { get; set; }
        public Nullable<int> MaNuocUong { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual Combo Combo { get; set; }
        public virtual NuocUong NuocUong { get; set; }
    }
}