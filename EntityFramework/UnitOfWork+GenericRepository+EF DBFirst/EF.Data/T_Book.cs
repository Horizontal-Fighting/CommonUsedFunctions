//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public Nullable<System.DateTime> Published { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CurrencyTypeID { get; set; }
    }
}
