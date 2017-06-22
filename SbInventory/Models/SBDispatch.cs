//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace SbInventory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SBDispatch
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime DRDate { get; set; }
        public System.DateTime DRTime { get; set; }
        [StringLength(15,ErrorMessage = "EID must be 15 character long",MinimumLength = 15)]
        public string EID { get; set; }
        public string Remarks { get; set; }
        public int SBDSBId { get; set; }
        public string Status { get; set; }
    
        public virtual SBDSB SBDSB { get; set; }
    }
}