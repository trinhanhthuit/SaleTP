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
    
    public partial class Image
    {
        public System.Guid ImageID { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.Guid> PathID { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}