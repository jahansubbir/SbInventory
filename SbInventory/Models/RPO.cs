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
    
    public partial class RPO
    {
        public RPO()
        {
            this.RPOLogins = new HashSet<RPOLogin>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    
        public virtual ICollection<RPOLogin> RPOLogins { get; set; }
    }
}
