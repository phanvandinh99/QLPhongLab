//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_HDPHONGLAB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THIETBI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THIETBI()
        {
            this.CHITIETPHIEUNHAPs = new HashSet<CHITIETPHIEUNHAP>();
            this.CHITIETPHIEUXUATs = new HashSet<CHITIETPHIEUXUAT>();
        }
    
        public string MATB { get; set; }
        public string TENTB { get; set; }
        public string QUICACH { get; set; }
        public Nullable<int> SLBANDAU { get; set; }
        public Nullable<int> SLXUAT { get; set; }
        public Nullable<int> SLTON { get; set; }
        public Nullable<int> SLTHANHLY { get; set; }
        public Nullable<bool> TAPHUAN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUXUAT> CHITIETPHIEUXUATs { get; set; }
        public virtual CT_THIETBI CT_THIETBI { get; set; }
        public virtual DUTRUTHIETBI DUTRUTHIETBI { get; set; }
    }
}