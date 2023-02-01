namespace QL_HDPHONGLAB.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHONGLAB")]
    public partial class PHONGLAB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONGLAB()
        {
            PHIEUMUON = new HashSet<PHIEUMUON>();
        }

        [Key]
        [StringLength(20)]
        public string MAPHLAB { get; set; }

        public string TENPHLAB { get; set; }

        public string DIADIEM { get; set; }

        public string GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUMUON> PHIEUMUON { get; set; }
    }
}
