//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sale.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class WhyUSDetail
    {
        public int WhyUSDetailID { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedByDate { get; set; }
        public Nullable<System.Guid> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}