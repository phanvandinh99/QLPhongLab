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
    
    public partial class SINHVIEN
    {
        public string MASV { get; set; }
        public string TENSV { get; set; }
        public Nullable<System.DateTime> NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public int MAPXPHLAB { get; set; }
    
        public virtual PHIEUXUATPHONGLAB PHIEUXUATPHONGLAB { get; set; }
    }
}
