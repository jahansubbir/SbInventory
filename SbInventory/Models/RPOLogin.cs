//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SbInventory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RPOLogin
    {
        public int Id { get; set; }
        public int RPOId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    
        public virtual RPO RPO { get; set; }
    }
}
