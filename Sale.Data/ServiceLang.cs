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
    
    public partial class ServiceLang
    {
        public System.Guid ServiceLangID { get; set; }
        public Nullable<System.Guid> ServiceID { get; set; }
        public string LangID { get; set; }
        public string ServiceName { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string LongContent { get; set; }
    }
}