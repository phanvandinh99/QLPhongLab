namespace QL_HDPHONGLAB.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HOACHAT
    {
        [Key]
        [StringLength(20)]
        public string MAHC { get; set; }

        public int? SOLO { get; set; }

        public string XUATXU { get; set; }

        public string DONVI { get; set; }

        public DateTime? NGAYNHAP { get; set; }

        public int? SOPHIEUNHAP { get; set; }

        public string DVCUNGCAP { get; set; }

        public DateTime? NGAYHETHAN { get; set; }
    }
}
