namespace QL_HDPHONGLAB.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUMUON")]
    public partial class PHIEUMUON
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPM { get; set; }

        public DateTime NGAYMUON { get; set; }

        public DateTime NGAYTRA { get; set; }

        public string NOIDUNG { get; set; }

        [StringLength(20)]
        public string MAHC { get; set; }

        public string NGUOITRA { get; set; }

        [StringLength(20)]
        public string MAPHLAB { get; set; }

        [StringLength(50)]
        public string TU { get; set; }

        [StringLength(50)]
        public string DEN { get; set; }

        public int? TRANGTHAI { get; set; }

        public string GHICHU { get; set; }

        public virtual HOACHAT HOACHAT { get; set; }

        public virtual PHONGLAB PHONGLAB { get; set; }
    }
}
