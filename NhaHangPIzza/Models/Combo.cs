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
    
    public partial class Combo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Combo()
        {
            this.ComBo_PhanAn = new HashSet<ComBo_PhanAn>();
            this.ComBo_PhanUong = new HashSet<ComBo_PhanUong>();
        }
    
        public int MaCombo { get; set; }
        public string TenCombo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComBo_PhanAn> ComBo_PhanAn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComBo_PhanUong> ComBo_PhanUong { get; set; }
    }
}