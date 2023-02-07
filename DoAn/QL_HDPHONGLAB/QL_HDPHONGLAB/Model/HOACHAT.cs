namespace QL_HDPHONGLAB.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOACHAT")]
    public partial class HOACHAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOACHAT()
        {
            PHIEUMUON = new HashSet<PHIEUMUON>();
        }

        [Key]
        [StringLength(20)]
        public string MAHC { get; set; }

        public string TENHC { get; set; }

        public int? MALHC { get; set; }

        public int? THONGSO { get; set; }

        public string CASNO { get; set; }

        public string DONVI { get; set; }

        public int? LUONGNHAP { get; set; }

        public double? LUONGXUAT { get; set; }

        public double? LUONGTON { get; set; }

        public int? LUONGTHANHLY { get; set; }

        public double? GIANHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUMUON> PHIEUMUON { get; set; }
    }
}
