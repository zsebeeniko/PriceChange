//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartPrice.DL.EFDataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductPrice
    {
        public int PRODUCT_ID { get; set; }
        public int PRICE_TYPE { get; set; }
        public decimal VALUE { get; set; }
    
        public virtual Price Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
