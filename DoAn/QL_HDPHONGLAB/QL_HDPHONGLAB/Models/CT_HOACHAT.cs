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
    
    public partial class CT_HOACHAT
    {
        public string MAHC { get; set; }
        public Nullable<int> SOLO { get; set; }
        public string XUATXU { get; set; }
        public string DONVI { get; set; }
        public Nullable<System.DateTime> NGAYNHAP { get; set; }
        public Nullable<int> SOPHIEUNHAP { get; set; }
        public string DVCUNGCAP { get; set; }
        public Nullable<System.DateTime> NGAYHETHAN { get; set; }
    
        public virtual HOACHAT HOACHAT { get; set; }
    }
}
