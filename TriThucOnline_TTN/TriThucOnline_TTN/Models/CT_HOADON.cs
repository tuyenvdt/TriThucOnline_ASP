//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TriThucOnline_TTN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CT_HOADON
    {
        public int Mahd { get; set; }
        public int Masach { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<double> GiamGia { get; set; }
        public Nullable<double> ThanhTien { get; set; }
    
        public virtual HOADON HOADON { get; set; }
        public virtual DAUSACH DAUSACH { get; set; }
    }
}
